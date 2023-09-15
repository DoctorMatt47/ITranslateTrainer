﻿using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ITranslateTrainer.Application.Common.Interfaces;
using ITranslateTrainer.Application.TranslationSheet;
using Moq;

namespace ITranslateTrainer.Tests.Application.Integration.Common.Mocks;

public static class TranslationSheetServiceMock
{
    private static Mock<ITranslationSheetService>? _mock;

    public static Mock<ITranslationSheetService> Get()
    {
        if (_mock is not null) return _mock;

        _mock = new Mock<ITranslationSheetService>();
        _mock.Setup(a => a.ParseTranslations(It.IsAny<Stream>()))
            .Returns<Stream>(_ => Task.FromResult(Enumerable.Empty<ParseTranslationResponse>()));

        return _mock;
    }
}
