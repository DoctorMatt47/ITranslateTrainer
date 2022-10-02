using System.Collections;
using ITranslateTrainer.Application.Common.Responses;
using ITranslateTrainer.Application.Translations.Commands;
using ITranslateTrainer.Application.Translations.Queries;
using ITranslateTrainer.Application.Translations.Responses;
using ITranslateTrainer.Application.TranslationSheet.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ITranslateTrainer.WebUI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TranslationsController : ControllerBase
{
    private readonly IMediator _mediator;

    public TranslationsController(IMediator mediator) => _mediator = mediator;

    [HttpGet]
    public async Task<IEnumerable<GetTranslationResponse>> Get(CancellationToken cancellationToken) =>
        await _mediator.Send(new GetTranslationsQuery(), cancellationToken);

    [HttpPut]
    public async Task<IntIdResponse> Put(PutTranslationCommand command, CancellationToken cancellationToken) =>
        await _mediator.Send(command, cancellationToken);

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        await _mediator.Send(new DeleteTranslationCommand(id), cancellationToken);
        return NoContent();
    }

    [HttpPut("sheet")]
    public async Task<IEnumerable> PutSheet(IFormFile sheet, CancellationToken cancellationToken) =>
        await _mediator.Send(new PutTranslationSheetCommand(sheet.OpenReadStream()), cancellationToken);
}