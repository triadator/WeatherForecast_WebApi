using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using Web_Api.Interfaces;
using Web_Api.OpenMeteo;
using WebApi.Db;

namespace Web_Api.Controllers
{
    [Route("api/stats")]
    [Authorize]
    [ApiController]
    public class ForecastController : Controller
    {
        private IForecastProvider _forecastProvider;

        public ForecastController(IForecastProvider forecastProvider)
        {
            _forecastProvider = forecastProvider;
        }

        [HttpGet("{city}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(WeatherForecast))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<WeatherForecast>> Api([FromRoute] string city)
        {          
            var forecast = await _forecastProvider.GetWeatherForecastAsync(city);                 
            return forecast == null? NotFound() : Ok(forecast);
        }
    }
}
