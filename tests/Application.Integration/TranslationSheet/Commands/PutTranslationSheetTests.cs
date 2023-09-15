using System.IO;
using System.Threading.Tasks;
using ITranslateTrainer.Application.TranslationSheet;
using ITranslateTrainer.Tests.Application.Integration.Common.Mocks;
using MediatR;
using Moq;
using Xunit;

namespace ITranslateTrainer.Tests.Application.Integration.TranslationSheet.Commands;

public class PutTranslationSheetTests
{
    // ReSharper disable once NotAccessedField.Local
    private readonly IMediator _mediator;

    public PutTranslationSheetTests(IMediator mediator) => _mediator = mediator;

    [Fact]
    public async Task ShouldCallParseTranslationsMethod()
    {
        var stream = Stream.Null;
        var command = new ImportTranslationSheetCommand(stream);

        await _mediator.Send(command);

        var mock = TranslationSheetServiceMock.Get();
        mock.Verify(m => m.ParseTranslations(stream), Times.Once());
    }
}
