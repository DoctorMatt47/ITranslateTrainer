using ITranslateTrainer.Application.Common.Responses;
using ITranslateTrainer.Application.Translations;
using ITranslateTrainer.Application.TranslationSheet;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using OneOf;

namespace ITranslateTrainer.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TranslationsController(ISender mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IEnumerable<TranslationResponse>> Get(CancellationToken cancellationToken) =>
        await mediator.Send(new GetTranslationsQuery(), cancellationToken);

    [HttpPut]
    public async Task<TranslationResponse> Put(PutTranslationCommand command, CancellationToken cancellationToken) =>
        await mediator.Send(command, cancellationToken);

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        await mediator.Send(new DeleteTranslationCommand(id), cancellationToken);
        return NoContent();
    }

    [HttpPut("sheet")]
    public async Task<IEnumerable<OneOf<TranslationResponse, ErrorResponse>>> PutSheet(
        IFormFile sheet,
        CancellationToken cancellationToken)
    {
        return await mediator.Send(new ImportTranslationSheetCommand(sheet.OpenReadStream()), cancellationToken);
    }
}
