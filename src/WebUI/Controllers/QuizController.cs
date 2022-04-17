using ITranslateTrainer.Application.Quiz.Queries;
using ITranslateTrainer.Application.Quiz.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ITranslateTrainer.WebUI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class QuizController : ControllerBase
{
    private readonly IMediator _mediator;

    public QuizController(IMediator mediator) => _mediator = mediator;

    [HttpGet]
    public async Task<IEnumerable<GetQuizResponse>> Get([FromQuery] GetQuizQuery query,
        CancellationToken cancellationToken) =>
        await _mediator.Send(query, cancellationToken);
}
