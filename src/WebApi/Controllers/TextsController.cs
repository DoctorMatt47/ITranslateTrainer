using ITranslateTrainer.Application.Texts;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ITranslateTrainer.WebApi.Controllers;

[ApiController]
[Route(template: "api/[controller]")]
public class TextsController(ISender mediator) : ControllerBase
{
    [HttpPatch("{id}")]
    public async Task<ActionResult> Patch(PatchTextCommand command, CancellationToken cancellationToken)
    {
        await mediator.Send(command, cancellationToken);
        return NoContent();
    }
}
