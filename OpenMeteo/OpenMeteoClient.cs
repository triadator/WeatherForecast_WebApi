using System;
using System.Text.Json;
using System.Globalization;
using System.Net.Http;
using Web_Api.Interfaces;

namespace Web_Api.OpenMeteo
{
    public class OpenMeteoClient : IForecastProvider
    {
        
        private readonly string _weatherApiUrl = "https://api.open-meteo.com/v1/forecast";
        private readonly string _geocodeApiUrl = "https://geocoding-api.open-meteo.com/v1/search";
        public HttpClient Client { get { return _httpClient; } }
        private readonly HttpClient _httpClient;
        public OpenMeteoClient()
        {
            _httpClient = new HttpClient();
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json")
                );
            _httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("om-dotnet");
        }
        public async Task<GeocodingApiResponse?> GetLocationDataAsync(string location)
        {
            GeocodingOptions options = new GeocodingOptions(location);

            try
            {
                HttpResponseMessage response = await Client.GetAsync(MergeUrlWithOptions(_geocodeApiUrl, options));
                response.EnsureSuccessStatusCode();

                GeocodingApiResponse? geocodingData = await JsonSerializer.DeserializeAsync<GeocodingApiResponse>(await response.Content.ReadAsStreamAsync(), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

                return geocodingData;
            }
            catch (HttpRequestException e)
            {                
                return null;
            }
        }
       
        public async Task<WeatherForecast?> GetWeatherForecastAsync(string location)
        {
            GeocodingApiResponse? georesponse = await GetLocationDataAsync(location);
            if (georesponse.Locations==null)
            {
                return new WeatherForecast() { LocationName = "Wrong location data"};
            }
            WeatherForecastOptions options = new WeatherForecastOptions(georesponse.Locations[0].Latitude, georesponse.Locations[0].Longitude);
            HttpResponseMessage response = await Client.GetAsync(MergeUrlWithOptions(_weatherApiUrl, options));
            WeatherForecast? weatherForecast = await JsonSerializer.DeserializeAsync<WeatherForecast>(await response.Content.ReadAsStreamAsync(), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
            weatherForecast.LocationName = georesponse.Locations[0].Country;
             return weatherForecast;
            
                    
        }
       //URL-Builder для Погоды
        private string MergeUrlWithOptions(string url, WeatherForecastOptions? options)
        {
            if (options == null) return url;

            UriBuilder uri = new UriBuilder(url);
            bool isFirstParam = false;
                       
            if (uri.Query == string.Empty)
            {
                uri.Query = "?";

                // isFirstParam becomes true because the query string is new
                isFirstParam = true;
            }

           
            if (isFirstParam)
                uri.Query += "latitude=" + options.Latitude.ToString(CultureInfo.InvariantCulture);
            else
                uri.Query += "&latitude=" + options.Latitude.ToString(CultureInfo.InvariantCulture);

            uri.Query += "&longitude=" + options.Longitude.ToString(CultureInfo.InvariantCulture);

            uri.Query += "&temperature_unit=" + options.Temperature_Unit.ToString();
            uri.Query += "&windspeed_unit=" + options.Windspeed_Unit.ToString();
            uri.Query += "&precipitation_unit=" + options.Precipitation_Unit.ToString();
            if (options.Timezone != string.Empty)
                uri.Query += "&timezone=" + options.Timezone;

            uri.Query += "&current_weather=" + options.Current_Weather;

            uri.Query += "&timeformat=" + options.Timeformat.ToString();

            uri.Query += "&past_days=" + options.Past_Days;

            if (options.Start_date != string.Empty)
                uri.Query += "&start_date=" + options.Start_date;
            if (options.End_date != string.Empty)
                uri.Query += "&end_date=" + options.End_date;
           
            // Daily
            if (options.Daily.Count > 0)
            {
                bool firstDailyElement = true;
                uri.Query += "&daily=";
                foreach (var option in options.Daily)
                {
                    if (firstDailyElement)
                    {
                        uri.Query += option.ToString();
                        firstDailyElement = false;
                    }
                    else
                    {
                        uri.Query += "," + option.ToString();
                    }
                }
            }
                       
            uri.Query += "&cell_selection=" + options.Cell_Selection;
                        
            if (options.Models.Count > 0)
            {
                bool firstModelsElement = true;
                uri.Query += "&models=";
                foreach (var option in options.Models)
                {
                    if (firstModelsElement)
                    {
                        uri.Query += option.ToString();
                        firstModelsElement = false;
                    }
                    else
                    {
                        uri.Query += "," + option.ToString();
                    }
                }
            }

            return uri.ToString();
        }

        //URL-Builder для Гео
        private string MergeUrlWithOptions(string url, GeocodingOptions options)
        {
            if (options == null) return url;

            UriBuilder uri = new UriBuilder(url);
            bool isFirstParam = false;

            if (uri.Query == string.Empty)
            {
                uri.Query = "?";

                isFirstParam = true;
            }

            if (isFirstParam)
                uri.Query += "name=" + options.Name;
            else
                uri.Query += "&name=" + options.Name;

            if (options.Count > 0)
                uri.Query += "&count=" + options.Count;

            if (options.Format != string.Empty)
                uri.Query += "&format=" + options.Format;

            if (options.Language != string.Empty)
                uri.Query += "&language=" + options.Language;

            return uri.ToString();
        }

     
    }
}
