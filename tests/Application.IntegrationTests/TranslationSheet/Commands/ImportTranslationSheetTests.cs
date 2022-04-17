﻿using System.IO;
using System.Threading.Tasks;
using ITranslateTrainer.Application.IntegrationTests.Common.Mocks;
using ITranslateTrainer.Application.TranslationSheet.Commands;
using MediatR;
using Moq;
using Xunit;

namespace ITranslateTrainer.Application.IntegrationTests.TranslationSheet.Commands;

public class ImportTranslationSheetTests
{
    // ReSharper disable once NotAccessedField.Local
    private readonly IMediator _mediator;

    public ImportTranslationSheetTests(IMediator mediator) => _mediator = mediator;

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