﻿using ITranslateTrainer.Application.Common.Extensions;
using ITranslateTrainer.Application.Common.Interfaces;
using ITranslateTrainer.Application.Common.Responses;
using ITranslateTrainer.Application.Quiz.Responses;
using ITranslateTrainer.Application.Texts.Extensions;
using ITranslateTrainer.Domain.Entities;
using ITranslateTrainer.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ITranslateTrainer.Application.Quiz.Queries;

public record GetQuizQuery(Language From, Language To, int TestCount, int OptionCount) :
    IQuery<IEnumerable<GetQuizResponse>>;

public record GetQuizQueryHandler(ITranslateDbContext _context) :
    IRequestHandler<GetQuizQuery, IEnumerable<GetQuizResponse>>
{
    private readonly ITranslateDbContext _context = _context;

    public async Task<IEnumerable<GetQuizResponse>> Handle(GetQuizQuery request,
        CancellationToken cancellationToken)
    {
        var (from, to, testCount, optionCount) = request;

        var textsToTranslate = await _context.Set<Text>()
            .GetRandomCanBeTested(from, testCount)
            .ToListAsync(cancellationToken);

        var randomOptions = await _context.Set<Text>()
            .GetRandomCanBeOption(to, optionCount)
            .ToListAsync(cancellationToken);

        var allCorrectOptions = textsToTranslate.Select(t => t.GetTranslationTexts());

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
