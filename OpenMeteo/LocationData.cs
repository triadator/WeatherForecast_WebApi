using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Web_Api.OpenMeteo
{

    public class LocationData
    {
     
        public int Id { get; set; }
             
        public string? Name { get; set; }

        public float Latitude { get; set; }

        public float Longitude { get; set; }

        public float Elevation { get; set; }

        public string? Timezone { get; set; }

        [JsonPropertyName("feature_code")]
        public string? FeatureCode { get; set; }

        [JsonPropertyName("country_code")]
        public string? CountryCode { get; set; }

        public string? Country { get; set; }

        [JsonPropertyName("country_id")]
        public int CountryId { get; set; }
               

    }
}
