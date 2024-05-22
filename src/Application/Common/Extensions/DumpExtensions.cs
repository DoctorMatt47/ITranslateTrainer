using System.Text.Json;

namespace ITranslateTrainer.Application.Common.Extensions;

public static class DumpExtensions
{
    private static readonly JsonSerializerOptions Options = new()
    {
        WriteIndented = true,
    };

    public static string Dump<T>(this T obj)
    {
        return JsonSerializer.Serialize(obj, Options);
    }
}
