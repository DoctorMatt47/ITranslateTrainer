using AutoMapper;
using ITranslateTrainer.Application.Common.Extensions;
using ITranslateTrainer.Application.Common.Interfaces;
using ITranslateTrainer.Application.Tests.Responses;
using ITranslateTrainer.Application.Texts.Extensions;
using ITranslateTrainer.Application.Texts.Queries;
using ITranslateTrainer.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ITranslateTrainer.Application.Tests.Commands;

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
        var test = await _context.Set<Test>().FirstOrDefaultAsync(
            t => !t.IsAnswered && t.OptionCount == optionCount,
            cancellationToken);

        if (test is not null) return _mapper.Map<GetOrCreateTestResponse>(test);

        var testedText = await _context.Set<Text>()
            .Where(t => t.CanBeTested && t.Language == from)
            .OrderBy(_ => EF.Functions.Random())
            .FirstAsync(cancellationToken);

        var correct = await _mediator.Send(new GetTranslationTextsByTextId(testedText.Id), cancellationToken);

        var incorrect = await _context.Set<Text>()
            .GetRandomCanBeOption(to, optionCount - correct.Count)
            .ToListAsync(cancellationToken);

        var optionTexts = incorrect.Concat(correct).Shuffle();

        test = new Test(testedText.Id, optionCount);
        await _context.Set<Test>().AddAsync(test, cancellationToken);

        var options = optionTexts.Select(t => new Option(t.Id, test.Id));
        await _context.Set<Option>().AddRangeAsync(options, cancellationToken);

        await _context.SaveChangesAsync();

        return _mapper.Map<GetOrCreateTestResponse>(test);
    }
}
