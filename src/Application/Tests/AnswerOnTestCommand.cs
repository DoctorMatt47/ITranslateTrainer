using AutoMapper;
using ITranslateTrainer.Application.Common.Exceptions;
using ITranslateTrainer.Application.Common.Interfaces;
using ITranslateTrainer.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ITranslateTrainer.Application.Tests;

public record AnswerOnTestCommand(
        int Id,
        int OptionId)
    : IRequest<TestResponse>;

internal class AnswerOnTestCommandHandler : IRequestHandler<AnswerOnTestCommand, TestResponse>
{
    private readonly ITranslateDbContext _dbContext;
    private readonly IMapper _mapper;

    public AnswerOnTestCommandHandler(ITranslateDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<TestResponse> Handle(AnswerOnTestCommand request, CancellationToken cancellationToken)
    {
        var test = await _dbContext.Set<Test>().FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken);
        if (test is null) throw new NotFoundException($"There is no test with id: {request.Id}");

        if (Test.IsAnswered.Compile().Invoke(test))
        {
            return _mapper.Map<TestResponse>(test);
        }

        var option = test.Options.FirstOrDefault(o => o.Id == request.OptionId);
        if (option is null) throw new NotFoundException($"There is no option with id: {request.OptionId}");

        var dateNow = DateOnly.FromDateTime(DateTime.UtcNow);

        var dayResult = await _dbContext.Set<DayResult>().FindAsync(dateNow)
            ?? _dbContext.Set<DayResult>().Add(new DayResult(dateNow)).Entity;

        option.Choose();
        test.Answer();
        dayResult.Answer(option.IsCorrect);
        test.TranslationText.Answer(option.IsCorrect);

        await _dbContext.SaveChangesAsync();

        return _mapper.Map<TestResponse>(test);
    }
}
