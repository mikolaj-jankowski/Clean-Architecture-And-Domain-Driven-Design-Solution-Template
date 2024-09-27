using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CA.And.DDD.Template.Infrastructure.Exceptions
{
    public class CommandValidationExceptionHandler : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            var result = new ProblemDetails();
            switch (exception.InnerException)
            {
                case CommandValidationException commandValidationException:
                    result = new ProblemDetails()
                    {
                        Status = (int)HttpStatusCode.BadRequest,
                        Type = exception.GetType().Name,
                        Title = "Validation errors",
                        Detail = "",
                        Instance = $"{httpContext.Request.Method} {httpContext.Request.Path}",
                        Extensions = new Dictionary<string, object?>
                        {
                            {
                                "errors", (commandValidationException as  CommandValidationException).Content
                            }
                        }
                    };
                    break;
                default:
                    result = new ProblemDetails
                    {
                        Status = (int)HttpStatusCode.InternalServerError,
                        Type = exception.GetType().Name,
                        Title = "An unexpected error occurred while processing your request.",
                        Detail = exception.Message,
                        Instance = $"{httpContext.Request.Method} {httpContext.Request.Path}"
                    };
                    break;
            }
            await httpContext.Response.WriteAsJsonAsync(result, cancellationToken: cancellationToken);
            return true;
        }
    }
}