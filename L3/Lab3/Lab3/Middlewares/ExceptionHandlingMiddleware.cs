using Lab3.Controllers;
using Lab3.Data.Models.Error;
using Lab3.Domain;
using Lab3.Utils;

namespace Lab3.Middlewares;

public class ExceptionHandlingMiddleware(ILogger<ExceptionHandlingMiddleware> logger) : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred: {Message}", ex.Message);
            await HandleExceptionAsync(context, ex);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var format = context.Request.RouteValues["format"]?.ToString() ?? "json";
        var errorCode = (int)(exception is LabException ex ? ex.Code : ErrorCode.Unexpected);

        var errorResponse = new ErrorResponseModel
        {
            Code = errorCode,
            Links =
            [
                new()
                {
                    Href =
                        $"{context.Request.Scheme}://{context.Request.Host}/api/{typeof(ErrorsController).GetControllerName()}.{format}/{errorCode}",
                    Rel = Rel.Details,
                    Method = HttpMethod.Get.Method,
                },
            ],
        };

        var result = new FormattedResult<ErrorResponseModel>(
            errorResponse,
            format,
            StatusCodes.Status400BadRequest
        );
        context.Response.StatusCode = result.StatusCode!.Value;
        context.Response.ContentType = result.ContentType;
        await context.Response.WriteAsync(result.Content!);
    }
}
