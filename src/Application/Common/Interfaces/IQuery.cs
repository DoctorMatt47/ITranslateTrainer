using MediatR;

namespace ITranslateTrainer.Application.Common.Interfaces;

public interface IQuery<out TResponse> : IRequest<TResponse>
{
}

public interface IQuery : IRequest
{
}