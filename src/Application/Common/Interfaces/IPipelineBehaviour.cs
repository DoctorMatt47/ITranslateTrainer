using MediatR;

namespace ITranslateTrainer.Application.Common.Interfaces;

public interface IPipelineBehavior<in TRequest> : IPipelineBehavior<TRequest, Unit> where TRequest : IRequest<Unit>
{
}
