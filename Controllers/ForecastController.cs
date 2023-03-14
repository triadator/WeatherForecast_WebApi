using Microsoft.AspNetCore.Mvc;
using Web_Api.Interfaces;
using Web_Api.OpenMeteo;

namespace Web_Api.Controllers
{
    [Route("api/stats")]
    [ApiController]
    public class ForecastController : Controller
    {
        private IForecastProvider _forecastProvider;
        public ForecastController(IForecastProvider forecastProvider)
        {
            this._forecastProvider = forecastProvider;
        }

        [HttpGet("{city}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(WeatherForecast))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<WeatherForecast>> Api(string city)
        {          
            var forecast = await _forecastProvider.GetWeatherForecastAsync(city);
            return forecast == null? NotFound() : Ok(forecast);
        }
    }
}
