using ITranslateTrainer.Application.Common.Interfaces;
using MediatR;

namespace ITranslateTrainer.Application.Common.Behaviours;

public sealed record CommandSaveBehaviour<TRequest, TResponse>(ITranslateDbContext _context) :
    IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>, ITransactional
{
    private readonly ITranslateDbContext _context = _context;

    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken,
        RequestHandlerDelegate<TResponse> next)
    {
        var response = await next.Invoke();
        await _context.SaveChangesAsync();
        return response;
    }
}