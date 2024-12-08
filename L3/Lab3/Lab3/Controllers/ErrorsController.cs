using System.Net.Mime;
using Lab3.Data.Models.Error;
using Lab3.Domain;
using Lab3.Utils;
using Microsoft.AspNetCore.Mvc;

namespace Lab3.Controllers;

[Route("api/[controller]")]
[Route("api/[controller].{format:regex(^(json|xml)$)}")]
[ApiController]
public class ErrorsController : ControllerBase
{
    [HttpGet("{errorCode}")]
    [Produces(MediaTypeNames.Application.Json, MediaTypeNames.Application.Xml)]
    [ProducesResponseType(typeof(ErrorDetailsResponseModel), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponseModel), StatusCodes.Status400BadRequest)]
    public IActionResult GetErrorDetailsByCode(int errorCode, string format = DataFormat.Json)
    {
        var errorMessage = (ErrorCode)errorCode switch
        {
            ErrorCode.StudentNotFoundById => "No student record with the given ID was found.",
            ErrorCode.ErrorCodeNotFound => "No error with the given code was found.",

            ErrorCode.Unexpected => "An unexpected error occurred.",
            ErrorCode.DatabaseError => "A database error occurred during the operation.",
            ErrorCode.StudentUpdateConcurrencyError =>
                "The student record you are trying to update has been modified or deleted by another user.",

            _ => throw new LabException(ErrorCode.ErrorCodeNotFound),
        };

        var errorDetailsModel = new ErrorDetailsResponseModel
        {
            Code = (int)errorCode,
            Message = errorMessage,
        };

        return new FormattedResult<ErrorDetailsResponseModel>(errorDetailsModel, format);
    }
}
