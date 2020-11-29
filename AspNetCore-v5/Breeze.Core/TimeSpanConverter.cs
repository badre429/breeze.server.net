using System;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Xml;

namespace Breeze.Core {
  // http://www.w3.org/TR/xmlschema-2/#duration

  public class TimeSpanConverter : JsonConverter<TimeSpan> {
    public override TimeSpan Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options) =>
            TimeSpan.Parse(reader.GetString());

    public override void Write(
        Utf8JsonWriter writer,
        TimeSpan dateTimeValue,
        JsonSerializerOptions options) =>
            writer.WriteStringValue(dateTimeValue.ToString());
  }
}


