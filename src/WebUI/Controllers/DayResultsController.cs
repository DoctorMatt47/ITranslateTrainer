using ITranslateTrainer.Application.DayResults;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ITranslateTrainer.WebUI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DayResultsController : ControllerBase
{
    private readonly IMediator _mediator;

    public DayResultsController(IMediator mediator) => _mediator = mediator;

    [HttpGet]
    public Task<IEnumerable<GetDayResultResponse>> GetDayResults(CancellationToken cancellationToken) =>
        _mediator.Send(new GetDayResultsQuery(), cancellationToken);
}
