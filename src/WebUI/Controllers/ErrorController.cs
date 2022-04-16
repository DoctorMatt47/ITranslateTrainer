using ITranslateTrainer.Application.Common.Exceptions;
using ITranslateTrainer.Domain.Exceptions;
using ITranslateTrainer.WebUI.Dtos.Responses;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace ITranslateTrainer.WebUI.Controllers;

[ApiExplorerSettings(IgnoreApi = true)]
public class ErrorController : Controller
{
    [Route("/error")]
    public ActionResult<object> HandleError()
    {
        var exception = HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error;
        var responseCode = exception switch
        {
            BadRequestException or DomainArgumentException => 400,
            NotFoundException => 404,
            _ => 500
        };
        return StatusCode(responseCode, new ErrorResponse(exception?.Message, exception?.StackTrace));
    }
}
