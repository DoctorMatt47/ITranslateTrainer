using ITranslateTrainer.Application.Texts;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ITranslateTrainer.WebApi.Controllers;

public record UpdateTextBody(string Text);

public class TextsController(ISender mediator) : ApiControllerBase
{
    [HttpPut("{id:int}")]
    public async Task<ActionResult> UpdateText(int id, UpdateTextBody body, CancellationToken cancellationToken)
    {
        var command = new UpdateTextCommand(id, body.Text);
        await mediator.Send(command, cancellationToken);
        return NoContent();
    }
}
