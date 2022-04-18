﻿using System.Text.Json;
using System.Text.Json.Serialization;
using ITranslateTrainer.Domain.ValueObjects;

namespace ITranslateTrainer.Domain.JsonConverters;

public class TextStringJsonConverter : JsonConverter<TextString>
{
    public override TextString Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) =>
        reader.GetString();

    public override void Write(Utf8JsonWriter writer, TextString value, JsonSerializerOptions options) =>
        writer.WriteStringValue(value.Value);
}