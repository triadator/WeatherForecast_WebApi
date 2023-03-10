using System.Text.Json.Serialization;

namespace Web_Api.OpenMeteo
{

    public class WeatherForecast
    {
        public string? LocationName { get; set; }
        public float Latitude { get; set; }

        public float Longitude { get; set; }

        public float Elevation { get; set; }
        [JsonPropertyName("generationtime_ms")]
        public float GenerationTime { get; set; }

        [JsonPropertyName("utc_offset_seconds")]
        public int UtcOffset { get; set; }

        public string? Timezone { get; set; }

        [JsonPropertyName("timezone_abbreviation")]
        public string? TimezoneAbbreviation { get; set; }

        [JsonPropertyName("current_weather")]
        public CurrentWeather? CurrentWeather { get; set; }
               
        [JsonPropertyName("daily_units")]
        public Daily_Units? Daily_units { get; set; }

        [JsonPropertyName("daily")]
        public Daily? Daily { get; set; }
    }
}


