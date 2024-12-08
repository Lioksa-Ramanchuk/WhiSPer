using System.Net.Mime;
using AutoMapper;
using Lab3.Data;
using Lab3.Data.Models.Error;
using Lab3.Data.Models.Link;
using Lab3.Data.Models.Student;
using Lab3.Domain;
using Lab3.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lab3.Controllers;

[Route("api/[controller]")]
[Route("api/[controller].{format:regex(^(json|xml)$)}")]
[ApiController]
public class StudentsController(ApplicationDbContext db, IMapper mapper) : ControllerBase
{
    [HttpGet("{studentId}")]
    [Produces(MediaTypeNames.Application.Json, MediaTypeNames.Application.Xml)]
    [ProducesResponseType(typeof(StudentResponseModel), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponseModel), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetStudentByIdAsync(
        [FromRoute] int studentId,
        [FromRoute] string format = DataFormat.Json,
        CancellationToken cancellationToken = default
    )
    {
        var student =
            await db.Students.FindAsync([studentId], cancellationToken)
            ?? throw new LabException(ErrorCode.StudentNotFoundById);

        var studentModel = mapper.Map<StudentResponseModel>(student);
        studentModel.Links = GenerateStudentLinks(format, studentId);

        return new FormattedResult<StudentResponseModel>(studentModel, format);
    }

    [HttpGet]
    [Produces(MediaTypeNames.Application.Json, MediaTypeNames.Application.Xml)]
    [ProducesResponseType(typeof(StudentsListResponseModel), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetStudentsAsync(
        [FromQuery] int? offset,
        [FromQuery] int? limit,
        [FromQuery] string? sort = null,
        [FromQuery] int? minid = null,
        [FromQuery] int? maxid = null,
        [FromQuery] string? columns = null,
        [FromQuery] string? like = null,
        [FromQuery] string? globalike = null,
        [FromRoute] string format = DataFormat.Json,
        CancellationToken cancellationToken = default
    )
    {
        offset ??= 0;
        limit ??= 10;
        if (limit < 0)
        {
            limit = 0;
        }

        (int offset, int limit) window;
        if (offset < 0)
        {
            var windowLimit = offset.Value + limit.Value;
            window = (0, windowLimit < 0 ? 0 : windowLimit);
        }
        else
        {
            window = (offset.Value, limit.Value);
        }

        var query = db.Students.AsNoTracking();

        if (minid is not null)
        {
            query = query.Where(s => s.Id >= minid.Value);
        }
        if (maxid is not null)
        {
            query = query.Where(s => s.Id <= maxid.Value);
        }
        if (!string.IsNullOrWhiteSpace(like))
        {
            query = query.Where(s => EF.Functions.Like(s.Name, '%' + like.Trim('%') + '%'));
        }
        if (!string.IsNullOrWhiteSpace(globalike))
        {
            var searchTerms = globalike
                .Split([',', ';', '|'], StringSplitOptions.RemoveEmptyEntries)
                .Select(term => $"%{term.Trim([' ', '%'])}%")
                .ToList();

            query = query.Where(s =>
                searchTerms.All(term =>
                    EF.Functions.Like(s.Id.ToString(), term)
                    || EF.Functions.Like(s.Name, term)
                    || EF.Functions.Like(s.Phone, term)
                )
            );
        }

        if (!string.IsNullOrWhiteSpace(sort))
        {
            var isDescending = sort.StartsWith('-');
            var sortField = sort.TrimStart('+', '-').ToUpper();

            query = sortField switch
            {
                "NAME" => isDescending
                    ? query.OrderByDescending(s => s.Name)
                    : query.OrderBy(s => s.Name),
                "ID" => isDescending
                    ? query.OrderByDescending(s => s.Id)
                    : query.OrderBy(s => s.Id),
                _ => query.OrderBy(s => s.Id),
            };
        }
        else
        {
            query = query.OrderBy(s => s.Id);
        }

        var totalStudents = await query.CountAsync(cancellationToken);

        query = query.Skip(window.offset).Take(window.limit);

        List<StudentDynamicModel> studentDynamicModels;
        if (columns is null)
        {
            studentDynamicModels = await query
                .Select(s => new StudentDynamicModel
                {
                    Id = s.Id,
                    StudentData = mapper.Map<StudentResponseModel>(s),
                })
                .ToListAsync(cancellationToken);
        }
        else
        {
            var selectedColumns = columns
                .Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                .ToList()
                .ConvertAll(col => col = col.ToUpper());

            studentDynamicModels = await query
                .Select(s => new StudentDynamicModel
                {
                    Id = s.Id,
                    StudentData = new StudentResponseModel
                    {
                        Id = selectedColumns.Contains("ID") ? s.Id : null,
                        Name = selectedColumns.Contains("NAME") ? s.Name : null,
                        Phone = selectedColumns.Contains("PHONE") ? s.Phone : null,
                    },
                })
                .ToListAsync(cancellationToken);
        }

        foreach (var model in studentDynamicModels)
        {
            model.StudentData.Links = GenerateStudentLinks(format, model.Id);
        }

        var studentsListModel = new StudentsListResponseModel
        {
            Students = studentDynamicModels.Select(model => model.StudentData).ToList(),
            Links = GeneratePaginationLinks(format, offset.Value, limit.Value, totalStudents),
        };

        return new FormattedResult<StudentsListResponseModel>(studentsListModel, format);
    }

    [HttpPost]
    [Consumes(MediaTypeNames.Application.Json, MediaTypeNames.Application.Xml)]
    [Produces(MediaTypeNames.Application.Json, MediaTypeNames.Application.Xml)]
    [ProducesResponseType(typeof(StudentResponseModel), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponseModel), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddStudentAsync(
        [FromBody] StudentAddReplaceRequestModel addModel,
        [FromRoute] string format = DataFormat.Json,
        CancellationToken cancellationToken = default
    )
    {
        var newStudent = mapper.Map<Student>(addModel);
        db.Students.Add(newStudent);

        try
        {
            await db.SaveChangesAsync(cancellationToken);
        }
        catch (DbUpdateException)
        {
            throw new LabException(ErrorCode.DatabaseError);
        }

        var studentModel = mapper.Map<StudentResponseModel>(newStudent);
        studentModel.Links = GenerateStudentLinks(format, studentModel.Id!.Value);

        return new FormattedResult<StudentResponseModel>(studentModel, format);
    }

    [HttpPut("{studentId}")]
    [Consumes(MediaTypeNames.Application.Json, MediaTypeNames.Application.Xml)]
    [Produces(MediaTypeNames.Application.Json, MediaTypeNames.Application.Xml)]
    [ProducesResponseType(typeof(StudentResponseModel), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponseModel), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateStudentAsync(
        [FromBody] StudentAddReplaceRequestModel replaceModel,
        [FromRoute] int studentId,
        [FromRoute] string format = DataFormat.Json,
        CancellationToken cancellationToken = default
    )
    {
        var student =
            await db.Students.FindAsync([studentId], cancellationToken)
            ?? throw new LabException(ErrorCode.StudentNotFoundById);

        mapper.Map(replaceModel, student);

        try
        {
            await db.SaveChangesAsync(cancellationToken);
        }
        catch (DbUpdateConcurrencyException)
        {
            throw new LabException(ErrorCode.StudentUpdateConcurrencyError);
        }
        catch (DbUpdateException)
        {
            throw new LabException(ErrorCode.DatabaseError);
        }

        var studentModel = mapper.Map<StudentResponseModel>(student);
        studentModel.Links = GenerateStudentLinks(format, studentId);

        return new FormattedResult<StudentResponseModel>(studentModel, format);
    }

    [HttpDelete("{studentId}")]
    [Produces(MediaTypeNames.Application.Json, MediaTypeNames.Application.Xml)]
    [ProducesResponseType(typeof(StudentResponseModel), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponseModel), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> RemoveStudentAsync(
        [FromRoute] int studentId,
        [FromRoute] string format = DataFormat.Json,
        CancellationToken cancellationToken = default
    )
    {
        var student =
            await db.Students.FindAsync([studentId], cancellationToken)
            ?? throw new LabException(ErrorCode.StudentNotFoundById);

        db.Students.Remove(student);

        try
        {
            await db.SaveChangesAsync(cancellationToken);
        }
        catch (DbUpdateConcurrencyException)
        {
            throw new LabException(ErrorCode.StudentNotFoundById);
        }
        catch (DbUpdateException)
        {
            throw new LabException(ErrorCode.DatabaseError);
        }

        var studentModel = mapper.Map<StudentResponseModel>(student);

        return new FormattedResult<StudentResponseModel>(studentModel, format);
    }

    private List<LinkResponseModel> GenerateStudentLinks(string format, int studentId)
    {
        return
        [
            new()
            {
                Href =
                    $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}/api/{typeof(StudentsController).GetControllerName()}.{format}/{studentId}",
                Rel = Rel.Self,
                Method = HttpMethod.Get.Method,
            },
            new()
            {
                Href =
                    $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}/api/{typeof(StudentsController).GetControllerName()}.{format}/{studentId}",
                Rel = Rel.Update,
                Method = HttpMethod.Put.Method,
            },
            new()
            {
                Href =
                    $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}/api/{typeof(StudentsController).GetControllerName()}.{format}/{studentId}",
                Rel = Rel.Delete,
                Method = HttpMethod.Delete.Method,
            },
        ];
    }

    private List<LinkResponseModel> GeneratePaginationLinks(
        string format,
        int offset,
        int limit,
        int total
    )
    {
        var links = new List<LinkResponseModel>();
        var request = HttpContext.Request;
        var controllerName = typeof(StudentsController).GetControllerName();

        if (offset > 0)
        {
            var prevOffset = offset - limit;
            links.Add(
                new LinkResponseModel
                {
                    Href = BuildPaginatedUrl(prevOffset, limit),
                    Rel = Rel.Prev,
                    Method = HttpMethod.Get.Method,
                }
            );
        }

        if (offset + limit < total)
        {
            var nextOffset = offset + limit;
            links.Add(
                new LinkResponseModel
                {
                    Href = BuildPaginatedUrl(nextOffset, limit),
                    Rel = Rel.Next,
                    Method = HttpMethod.Get.Method,
                }
            );
        }

        return links;

        string BuildPaginatedUrl(int newOffset, int newLimit)
        {
            var uriBuilder = new UriBuilder(
                $"{request.Scheme}://{request.Host}/api/{controllerName}.{format}"
            );
            var query = Microsoft.AspNetCore.WebUtilities.QueryHelpers.ParseQuery(
                request.QueryString.ToString()
            );

            var queryDict = new Dictionary<string, string>(
                query.SelectMany(q =>
                    q.Value.Select(value => new KeyValuePair<string, string>(
                        q.Key,
                        value ?? string.Empty
                    ))
                )
            )
            {
                ["offset"] = newOffset.ToString(),
                ["limit"] = newLimit.ToString(),
            };

            uriBuilder.Query = string.Join("&", queryDict.Select(q => $"{q.Key}={q.Value}"));
            return uriBuilder.ToString();
        }
    }
}
