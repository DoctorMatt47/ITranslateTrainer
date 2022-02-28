using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using ITranslateTrainer.Application.Common.Exceptions;
using ITranslateTrainer.Application.Common.Interfaces;
using ITranslateTrainer.Application.Common.Responses;
using ITranslateTrainer.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ITranslateTrainer.Application.Translations.Commands
{
    public record CreateTranslationCommand(CreateTextRequest FirstText, CreateTextRequest SecondText) :
        IRequest<IntIdResponse>;

    public record CreateTranslationCommandHandler(ITranslateDbContext Context, IMapper Mapper) :
        IRequestHandler<CreateTranslationCommand, IntIdResponse>
    {
        public async Task<IntIdResponse> Handle(CreateTranslationCommand request, CancellationToken cancellationToken)
        {
            var (firstTextRequest, secondTextRequest) = request;

            var firstText = await Context.Texts.FirstOrDefaultAsync(
                t => t.String == firstTextRequest.String && t.Language == firstTextRequest.Language,
                cancellationToken);

            var secondText = await Context.Texts.FirstOrDefaultAsync(
                t => t.String == secondTextRequest.String && t.Language == secondTextRequest.Language,
                cancellationToken);

            if (firstText is not null && secondText is not null)
            {
                var translation = await Context.Translations.FirstOrDefaultAsync(
                    t => t.First == firstText && t.Second == secondText,
                    cancellationToken);
                if (translation is not null) return new IntIdResponse(translation.Id);
            }

            if (firstText is null)
            {
                firstText = Mapper.Map<Text>(firstTextRequest);
                await Context.Texts.AddAsync(firstText, cancellationToken);
            }

            if (secondText is null)
            {
                secondText = Mapper.Map<Text>(secondTextRequest);
                await Context.Texts.AddAsync(secondText, cancellationToken);
            }

            var translationToAdd = new Translation {First = firstText, Second = secondText};
            await Context.Translations.AddAsync(translationToAdd, cancellationToken);

            await Context.SaveChangesAsync();
            return new IntIdResponse(translationToAdd.Id);
        }
    }
}
