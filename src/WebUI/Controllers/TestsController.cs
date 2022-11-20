﻿using ITranslateTrainer.Application.Tests.Commands;
using ITranslateTrainer.Application.Tests.Queries;
using ITranslateTrainer.Application.Tests.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ITranslateTrainer.WebUI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TestsController : ControllerBase
{
    private readonly IMediator _mediator;

    public TestsController(IMediator mediator) => _mediator = mediator;

    [HttpGet("{id:int}")]
    public Task<GetTestResponse> GetTest(
        int id,
        CancellationToken cancellationToken) =>
        _mediator.Send(new GetTestQuery(id), cancellationToken);

    [HttpPut]
    public Task<GetOrCreateTestResponse> CreateTest(
        GetOrCreateTestCommand command,
        CancellationToken cancellationToken) =>
        _mediator.Send(command, cancellationToken);

    [HttpPut("{id:int}/Answer")]
    public Task AnswerOnTest(
        int id,
        AnswerOnTestBody body,
        CancellationToken cancellationToken)
    {
        var command = new AnswerOnTestCommand(id, body.OptionId);
        return _mediator.Send(command, cancellationToken);
    }
}

public record AnswerOnTestBody(int OptionId);