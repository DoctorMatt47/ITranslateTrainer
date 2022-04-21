using ITranslateTrainer.WebUI.JsonConverters;

namespace ITranslateTrainer.WebUI.Extensions;

public static class MvcBuilderExtensions
{
    public static IMvcBuilder AddJsonConverters(this IMvcBuilder builder) =>
        builder.AddJsonOptions(opts => opts.JsonSerializerOptions.Converters.Add(new StringJsonConverter()));
}