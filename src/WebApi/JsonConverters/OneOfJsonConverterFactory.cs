using System.Text.Json;
using System.Text.Json.Serialization;
using OneOf;

namespace ITranslateTrainer.WebApi.JsonConverters;

public class OneOfJsonConverterFactory : JsonConverterFactory
{
    public override bool CanConvert(Type typeToConvert) => typeToConvert.IsAssignableTo(typeof(IOneOf));

    public override JsonConverter? CreateConverter(Type typeToConvert, JsonSerializerOptions options)
    {
        var arguments = typeToConvert.GetGenericArguments();
        var converterType = typeof(OneOfJsonConverter<,>).MakeGenericType(arguments);
        return (JsonConverter?) Activator.CreateInstance(converterType);
    }
}

public class OneOfJsonConverter<T1, T2> : JsonConverter<OneOf<T1, T2>>
{
    public override OneOf<T1, T2> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) =>
        throw new NotImplementedException();

    public override void Write(Utf8JsonWriter writer, OneOf<T1, T2> value, JsonSerializerOptions options)
    {
        var jsonValue = JsonSerializer.Serialize(value.Value, options);
        writer.WriteRawValue(jsonValue);
    }
}
