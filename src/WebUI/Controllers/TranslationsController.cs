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
    public async Task<IEnumerable<GetTranslationResponse>> GetTranslations() =>
        await _mediator.Send(new GetTranslationsQuery());

    [HttpPost]
    public async Task<UintIdResponse> CreateTranslation(CreateTranslationCommand command) =>
        await _mediator.Send(command);

    [HttpPost("sheet")]
    public async Task<IEnumerable<object>> CreateTranslationSheet(IFormFile sheet) =>
        await _mediator.Send(new ImportTranslationSheetCommand(sheet.OpenReadStream()));
}