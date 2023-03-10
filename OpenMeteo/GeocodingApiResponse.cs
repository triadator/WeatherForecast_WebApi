using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Web_Api.OpenMeteo
{
    public class GeocodingApiResponse
    {     
        [JsonPropertyName("results")]
        public LocationData[]? Locations { get; set; }
                
        [JsonPropertyName("generationtime_ms")]
        public float GenerationTime { get; set; }
    }
}