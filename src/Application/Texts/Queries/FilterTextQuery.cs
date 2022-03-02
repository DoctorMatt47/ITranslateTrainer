using ITranslateTrainer.Application.Common.Exceptions;
using MediatR;

namespace ITranslateTrainer.Application.Texts.Queries;

public record FilterTextQuery(string? TextString) : IRequest<string>;

public record FilterTextQueryHandler : IRequestHandler<FilterTextQuery, string>
{
    public Task<string> Handle(FilterTextQuery request, CancellationToken _)
    {
        if (request.TextString is null) throw new BadRequestException("Text string is null");
        return Task.FromResult(request.TextString
            .Replace("\n", "")
            .Replace("\t", "")
            .Trim());
    }
}
