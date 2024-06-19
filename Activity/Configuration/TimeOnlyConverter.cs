using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Activity.Configuration {

    public class TimeOnlyConverter : JsonConverter<TimeOnly> {
        private const string TimeFormat = "HH:mm";

        public override TimeOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) {
            var timeAsString = reader.GetString();
            return TimeOnly.ParseExact(timeAsString, TimeFormat, CultureInfo.InvariantCulture);
        }

        public override void Write(Utf8JsonWriter writer, TimeOnly value, JsonSerializerOptions options) {
            writer.WriteStringValue(value.ToString(TimeFormat));
        }
    }
}