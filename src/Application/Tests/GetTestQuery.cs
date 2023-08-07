using AutoMapper;
using ITranslateTrainer.Application.Common.Exceptions;
using ITranslateTrainer.Application.Common.Interfaces;
using ITranslateTrainer.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ITranslateTrainer.Application.Tests;

public record GetTestQuery(int Id) : IRequest<TestResponse>;

internal record GetTestQueryHandler : IRequestHandler<GetTestQuery, TestResponse>
{
    private readonly ITranslateDbContext _context;
    private readonly IMapper _mapper;

    public GetTestQueryHandler(ITranslateDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<TestResponse> Handle(GetTestQuery request, CancellationToken cancellationToken)
    {
        var test = await _context.Set<Test>()
            .Include(t => t.TranslationText)
            .Include(t => t.Options)
            .ThenInclude(t => t.TranslationText)
            .FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken);

        if (test is null) throw new NotFoundException($"There is no test with id: {request.Id}");
        if (!Test.IsAnswered.Compile().Invoke(test))
        {
            throw new BadRequestException("You don't have permission to get not answered test");
        }

        return _mapper.Map<TestResponse>(test);
    }
}
