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
        var test = await _dbContext.Set<Test>().FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken)
            ?? throw new NotFoundException($"There is no test with id: {request.Id}");

        if (Test.IsAnswered(test))
        {
            return _mapper.Map<TestResponse>(test);
        }
        
        test.Answer(request.OptionId);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return _mapper.Map<TestResponse>(test);
    }
}
