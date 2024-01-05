using ITranslateTrainer.Application.Tests;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ITranslateTrainer.WebApi.Controllers;

public record AnswerOnTestBody(int OptionId);

[ApiController]
[Route("api/[controller]")]
public class TestsController(ISender mediator) : ControllerBase
{
    [HttpGet]
    public Task<IEnumerable<TestResponse>> GetTests(CancellationToken cancellationToken) =>
        mediator.Send(new GetTestsQuery(), cancellationToken);

    [HttpGet("{id:int}")]
    public Task<TestResponse> GetTest(
        int id,
        CancellationToken cancellationToken) => mediator.Send(new GetTestQuery(id), cancellationToken);

    [HttpPut]
    public Task<TestResponse> CreateTest(
        GetOrCreateTestCommand command,
        CancellationToken cancellationToken) => mediator.Send(command, cancellationToken);

    [HttpPut("{id:int}/Answer")]
    public Task<TestResponse> AnswerOnTest(
        int id,
        AnswerOnTestBody body,
        CancellationToken cancellationToken)
    {
        var command = new AnswerOnTestCommand(id, body.OptionId);
        return mediator.Send(command, cancellationToken);
    }
}
