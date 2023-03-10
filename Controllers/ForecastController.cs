using Microsoft.AspNetCore.Mvc;
using Web_Api.OpenMeteo;

namespace Web_Api.Controllers
{
    [Route("api/stats")]
    [ApiController]
    public class ForecastController : Controller
    {       
        [HttpGet("{city}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(WeatherForecast))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<WeatherForecast>> Api(string city)
        {            
            MeteoClient client = new MeteoClient();
            var forecast = await client.GetWeatherForecastAsync(city);
            return forecast == null? NotFound() : Ok(forecast);
        }
    }
}
