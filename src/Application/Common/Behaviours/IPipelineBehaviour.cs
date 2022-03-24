using MediatR;

namespace ITranslateTrainer.Application.Common.Behaviours;

public interface IPipelineBehavior<in TRequest> : IPipelineBehavior<TRequest, Unit> where TRequest : IRequest<Unit>
{
}