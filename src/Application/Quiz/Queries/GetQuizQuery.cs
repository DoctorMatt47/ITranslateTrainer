using ITranslateTrainer.Application.Common.Extensions;
using ITranslateTrainer.Application.Common.Responses;
using ITranslateTrainer.Application.Texts.Queries;
using ITranslateTrainer.Application.Translations.Queries;
using ITranslateTrainer.Domain.Enums;
using MediatR;

namespace ITranslateTrainer.Application.Quiz.Queries;

public record GetQuizQuery(Language From, Language To, int TestCount, int OptionCount) :
    IRequest<IEnumerable<GetQuizResponse>>;

public record GetQuizQueryHandler(IMediator Mediator) :
    IRequestHandler<GetQuizQuery, IEnumerable<GetQuizResponse>>
{
    public async Task<IEnumerable<GetQuizResponse>> Handle(GetQuizQuery request, CancellationToken cancellationToken)
    {
        var textsToTranslate = (await Mediator.Send(new GetRandomTextsByConditionQuery(
            t => t.Language == request.From && t.CanBeTested,
            request.TestCount), cancellationToken)).ToList();

        var randomTexts = (await Mediator.Send(new GetRandomTextsByConditionQuery(
            t => t.Language == request.To && t.CanBeOption,
            request.OptionCount * request.TestCount), cancellationToken)).ToList();

        var allCorrectOptions = await textsToTranslate
            .Select(t => Mediator.Send(new GetTranslationsByTextIdQuery(t.Id), cancellationToken))
            .WhenAllAsync();

        var allOptions = allCorrectOptions.Select(correctOptions =>
        {
            var list = correctOptions.ToList();
            var count = list.Count;

            // If count of correct translations more or equal than count of options
            // then takes only correct translations, limited by options count.
            if (count >= request.OptionCount)
                return list.Take(request.OptionCount).Select(o => new OptionResponse(o.String, true));

            // Finds options which have not correct translations and shuffles them.
            // Takes only general count minus correct count of incorrect options.
            // Concatenates with correct options.
            // Shuffles resulting collection.
            return randomTexts.Where(r => !list.Select(o => o.Id).Contains(r.Id)).Shuffle()
                .Take(request.OptionCount - count).Select(o => new OptionResponse(o.String, false))
                .Concat(list.Select(o => new OptionResponse(o.String, true))).Shuffle();
        });

        return textsToTranslate.Zip(allOptions, (t, o) => new GetQuizResponse(t.String, o));
    }
}
