using ITranslateTrainer.Domain.Entities;
using Riok.Mapperly.Abstractions;

namespace ITranslateTrainer.Application.Tests;

[Mapper]
public static partial class TestMapper
{
    public static partial IQueryable<TestResponse> ProjectToResponse(this IQueryable<Test> tests);

    [MapProperty(new[] {nameof(Test.Text), nameof(Test.Text.Value)}, new[] {nameof(TestResponse.Text)})]
    public static partial TestResponse ToResponse(this Test test);

    [MapProperty(new[] {nameof(Option.Text), nameof(Option.Text.Value)}, new[] {nameof(OptionResponse.Text)})]
    private static partial OptionResponse ToResponse(this Option test);
}
