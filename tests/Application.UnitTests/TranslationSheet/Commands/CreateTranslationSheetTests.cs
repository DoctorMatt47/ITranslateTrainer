using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ITranslateTrainer.Application.TranslationSheet.Commands;
using ITranslateTrainer.Domain.Interfaces;
using MediatR;
using Xunit;

namespace ITranslateTrainer.Application.UnitTests.TranslationSheet.Commands;

public class CreateTranslationSheetTests
{
    private readonly IMediator _mediator;

    public CreateTranslationSheetTests(IMediator mediator, ITranslateDbContext context) => _mediator = mediator;

    [Fact]
    public async Task ShouldReturnResultOfTranslationCreation()
    {
        var response = await _mediator.Send(new CreateTranslationSheetCommand(File.OpenRead("Assets/TestSheet.xlsx")));
        Assert.Equal(233, response.Count());
    }
}