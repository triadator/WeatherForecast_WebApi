using Web_Api.OpenMeteo;

namespace Web_Api.Interfaces
{
    public interface IForecastProvider
    {
        public Task<WeatherForecast?> GetWeatherForecastAsync(string location);
    }
}
