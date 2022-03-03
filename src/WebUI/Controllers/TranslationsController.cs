using ITranslateTrainer.Application.Common.Responses;
using ITranslateTrainer.Application.Translations.Commands;
using ITranslateTrainer.Application.Translations.Queries;
using ITranslateTrainer.Application.TranslationSheet.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ITranslateTrainer.WebUI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TranslationsController : Controller
{
    private readonly IMediator _mediator;

    public TranslationsController(IMediator mediator) => _mediator = mediator;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<GetTranslationResponse>>> GetTranslations() =>
        Ok(await _mediator.Send(new GetTranslationsQuery()));

    [HttpPost]
    public async Task<ActionResult<IntIdResponse>> CreateTranslation(CreateTranslationCommand command) =>
        Ok(await _mediator.Send(command));

    [HttpPost("sheet")]
    public async Task<ActionResult<IEnumerable<object>>> CreateTranslationSheet(IFormFile sheet) =>
        Ok(await _mediator.Send(new CreateTranslationSheetCommand(sheet.OpenReadStream())));
}
