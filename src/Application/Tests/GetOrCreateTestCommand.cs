using AutoMapper;
using ITranslateTrainer.Application.Common.Extensions;
using ITranslateTrainer.Application.Common.Interfaces;
using ITranslateTrainer.Application.Texts;
using ITranslateTrainer.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ITranslateTrainer.Application.Tests;

public record GetOrCreateTestCommand(
        string From,
        string To,
        int OptionCount)
    : IRequest<GetOrCreateTestResponse>;

internal class CreateTestCommandHandler : IRequestHandler<GetOrCreateTestCommand, GetOrCreateTestResponse>
{
    private readonly ITranslateDbContext _context;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public CreateTestCommandHandler(ITranslateDbContext context, IMediator mediator, IMapper mapper)
    {
        _context = context;
        _mediator = mediator;
        _mapper = mapper;
    }

    public async Task<GetOrCreateTestResponse> Handle(
        GetOrCreateTestCommand request,
        CancellationToken cancellationToken)
    {
        var (from, to, optionCount) = request;
        var test = await _context.Set<Test>()
            .Where(Test.IsGotAnswer)
            .FirstOrDefaultAsync(t => t.OptionCount == optionCount, cancellationToken);

        if (test is not null) return _mapper.Map<GetOrCreateTestResponse>(test);

        var testedText = await _context.Set<Text>()
            .Where(t => t.CanBeTested && t.Language == from)
            .OrderBy(_ => EF.Functions.Random())
            .FirstAsync(cancellationToken);

        test = new Test(testedText.Id, optionCount);
        await _context.Set<Test>().AddAsync(test, cancellationToken);

        var correct = (await _mediator.Send(new GetTranslationTextsByTextId(testedText.Id), cancellationToken))
            .Select(text => new Option(text.Id, test.Id, true))
            .ToList();

        var incorrect = await _context.Set<Text>()
            .GetRandomCanBeOption(to, optionCount - correct.Count)
            .Select(t => new Option(t.Id, test.Id, false))
            .ToListAsync(cancellationToken);

        var options = correct.Concat(incorrect).Shuffle();
        await _context.Set<Option>().AddRangeAsync(options, cancellationToken);
        await _context.SaveChangesAsync();

        return _mapper.Map<GetOrCreateTestResponse>(test);
    }
}
