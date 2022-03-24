using ITranslateTrainer.Application.Common.Extensions;
using ITranslateTrainer.Application.Common.Responses;
using ITranslateTrainer.Application.Quiz.Responses;
using ITranslateTrainer.Application.Texts.Requests;
using ITranslateTrainer.Domain.Enums;
using MediatR;

namespace ITranslateTrainer.Application.Quiz.Queries;

public record GetQuizQuery(Language From, Language To, int TestCount, int OptionCount) :
    IRequest<IEnumerable<GetQuizResponse>>;

public record GetQuizQueryHandler(IMediator _mediator) :
    IRequestHandler<GetQuizQuery, IEnumerable<GetQuizResponse>>
{
    private readonly IMediator _mediator = _mediator;

    public async Task<IEnumerable<GetQuizResponse>> Handle(GetQuizQuery request,
        CancellationToken cancellationToken)
    {
        var (from, to, testCount, optionCount) = request;

        var textsToTranslate = (await _mediator.Send(new GetRandomCanBeTestedTexts(testCount, from),
            cancellationToken)).ToList();

        var randomOptions = (await _mediator.Send(new GetRandomCanBeOptionTexts(optionCount * testCount, to),
            cancellationToken)).ToList();

        var allCorrectOptions = await textsToTranslate
            .Select(t => _mediator.Send(new GetTranslationTextsByTextId(t.Id), cancellationToken))
            .WhenAllAsync();

        var allOptions = allCorrectOptions.Select(opts =>
        {
            var correctOptions = opts.ToList();
            var count = correctOptions.Count;

            // If count of correct translations more or equal than count of options
            // then takes only correct translations, limited by options count.
            if (count >= optionCount)
                return correctOptions.Take(optionCount).Select(o => new OptionResponse(o.String, true));

            // Finds options without correct translations and shuffles them.
            // Takes only general number minus correct number of incorrect options.
            var incorrectOptions = randomOptions.ExceptBy(correctOptions, opt => opt.Id).Shuffle()
                .Take(optionCount - count);

            // Concatenates incorrect and correct options.
            // Shuffles resulted collection.
            return incorrectOptions.Select(o => new OptionResponse(o.String, false))
                .Concat(correctOptions.Select(o => new OptionResponse(o.String, true)))
                .Shuffle();
        });

        return textsToTranslate.Zip(allOptions, (t, o) => new GetQuizResponse(t.String, o));
    }
}