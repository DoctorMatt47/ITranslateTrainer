using ITranslateTrainer.Application.Texts;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ITranslateTrainer.WebApi.Controllers;

public record PatchTextBody(string Text);

public class TextsController(ISender mediator) : ApiControllerBase
{
    [HttpPatch("{id:int}")]
    public async Task<ActionResult> Patch(int id, PatchTextBody body, CancellationToken cancellationToken)
    {
        var command = new PatchTextCommand(id, body.Text);
        await mediator.Send(command, cancellationToken);
        return NoContent();
    }
}
