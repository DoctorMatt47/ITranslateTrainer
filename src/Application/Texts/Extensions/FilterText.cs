using ITranslateTrainer.Application.Common.Exceptions;
using MediatR;

namespace ITranslateTrainer.Application.Texts.Extensions;

public record FilterText(string? TextString) : IRequest<string>;

public record FilterTextHandler : IRequestHandler<FilterText, string>
{
    public Task<string> Handle(FilterText request, CancellationToken _) =>
        Task.FromResult(request.TextString!.ToLower().Replace("\n", "").Replace("\t", "").Trim());
}

public record FilterTextValidateBehaviour : IPipelineBehavior<FilterText, string>
{
    public async Task<string> Handle(FilterText request, CancellationToken cancellationToken,
        RequestHandlerDelegate<string> next)
    {
        if (request.TextString is null) throw new BadRequestException("Text string is null");
        return await next.Invoke();
    }
}