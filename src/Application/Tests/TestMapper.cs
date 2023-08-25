using ITranslateTrainer.Domain.Entities;
using Riok.Mapperly.Abstractions;

namespace ITranslateTrainer.Application.Tests;

[Mapper]
public static partial class TestMapper
{
    public static partial IQueryable<TestResponse> ProjectToResponse(this IQueryable<Test> tests);

    [MapProperty($"{nameof(Test.Text)}.{nameof(Text.Value)}", nameof(TestResponse.Text))]
    public static partial TestResponse ToResponse(this Test test);

    [MapProperty($"{nameof(Option.Text)}.{nameof(Text.Value)}", nameof(OptionResponse.Text))]
    public static partial OptionResponse ToResponse(this Option test);
}
