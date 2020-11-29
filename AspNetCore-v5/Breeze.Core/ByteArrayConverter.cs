
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

// only needed because this functionality seems to be broken in JSON.NET 10.0.3
namespace Breeze.Core {
  public class ByteArrayConverter : JsonConverter<Byte[]> {
    public override bool CanConvert(Type objectType) {
      return objectType == typeof(byte[]);
    }

    public override Byte[] Read(
            ref Utf8JsonReader reader,
            Type typeToConvert,
            JsonSerializerOptions options) {
      if (reader.TokenType == JsonTokenType.Null)
        return null;

      switch (reader.TokenType) {
        case JsonTokenType.Null:
          return null;
        case JsonTokenType.String:
          return Convert.FromBase64String((string)reader.GetString());
        case JsonTokenType.StartArray: {
            var value = reader.GetString();
            return value == null ? null : Convert.FromBase64String(value);
          }
        default:
          throw new System.Text.Json.JsonException("Unknown byte array format");
      }
    }


    public override void Write(Utf8JsonWriter writer,
            Byte[] value,
            JsonSerializerOptions options) {
      string base64String = Convert.ToBase64String((byte[])value);

      writer.WriteStringValue(base64String);
    }
  }


}
