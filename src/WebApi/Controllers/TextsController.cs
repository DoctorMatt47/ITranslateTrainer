using ITranslateTrainer.Application.TranslationTexts;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ITranslateTrainer.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TextsController : ControllerBase
{
    private readonly IMediator _mediator;

    public TextsController(IMediator mediator) => _mediator = mediator;

    [HttpPatch]
    public async Task<ActionResult> Patch(PatchTextCommand command, CancellationToken cancellationToken)
    {
        await _mediator.Send(command, cancellationToken);
        return NoContent();
    }
}
