
using System.Runtime.Serialization.Formatters;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Breeze.Core {
  public static class JsonSerializationFns {

    public static JsonSerializerOptions UpdateWithDefaults(JsonSerializerOptions ss) {
      // ss.NullValueHandling = NullValueHandling.Include;
      ss.ReferenceHandler = ReferenceHandler.Preserve;
      // ss.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
      // ss.TypeNameHandling = TypeNameHandling.Objects;
      // ss.TypeNameAssemblyFormat = FormatterAssemblyStyle.Simple;
      // ss.TypeNameAssemblyFormatHandling = TypeNameAssemblyFormatHandling.Simple;

      // DateTime format is:  yyyy-MM-ddTHH:mm:ss.fff
      // DateTime2 format is: yyyy-MM-ddTHH:mm:ss.fffffffK

      // Hack is for the issue described in this post:
      // http://stackoverflow.com/questions/11789114/internet-explorer-json-net-javascript-date-and-milliseconds-issue
      // See also: https://stackoverflow.com/questions/52048935/how-do-i-get-entityframeworkcore-generated-sql-to-use-the-right-format-for-datet
      // ss.Converters.Add(new IsoDateTimeConverter {
      //   // datetime2
      //   DateTimeFormat = "yyyy-MM-dd\\THH:mm:ss.fffffffK"
      //   // datetime
      //   // DateTimeFormat = "yyyy-MM-dd\\THH:mm:ss.fffK"
      //   // old 
      //   // DateTimeFormat = "yyyy'-'MM'-'dd'T'HH':'mm':'ss.FFFFFFFK"
      // });


      // Needed because JSON.NET does not natively support I8601 Duration formats for TimeSpan
      ss.Converters.Add(new TimeSpanConverter());
      ss.Converters.Add(new JsonStringEnumConverter());
      // only needed because this functionality seems to be broken in JSON.NET 10.0.3
      ss.Converters.Add(new ByteArrayConverter());

      // Default is DateTimeZoneHandling.RoundtripKind - you can change that here.
      // ss.DateTimeZoneHandling = DateTimeZoneHandling.Utc;

      return ss;
    }
  }
}
