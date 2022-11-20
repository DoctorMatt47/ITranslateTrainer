using AutoMapper;
using ITranslateTrainer.Application.Common.Exceptions;
using ITranslateTrainer.Application.Common.Interfaces;
using ITranslateTrainer.Application.Tests.Responses;
using ITranslateTrainer.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ITranslateTrainer.Application.Tests.Queries;

public record GetTestQuery(int Id) : IRequest<GetTestResponse>;

internal record GetTestQueryHandler : IRequestHandler<GetTestQuery, GetTestResponse>
{
    private readonly ITranslateDbContext _context;
    private readonly IMapper _mapper;

    public GetTestQueryHandler(ITranslateDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<GetTestResponse> Handle(GetTestQuery request, CancellationToken cancellationToken)
    {
        var test = await _context.Set<Test>()
            .Include(t => t.Text)
            .Include(t => t.Options)
            .ThenInclude(t => t.Text)
            .FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken);

        if (test is null) throw new NotFoundException($"There is no test with id: {request.Id}");
        if (!test.IsAnswered) throw new BadRequestException("You don't have permission to get not answered test");

        return _mapper.Map<GetTestResponse>(test);
    }
}
