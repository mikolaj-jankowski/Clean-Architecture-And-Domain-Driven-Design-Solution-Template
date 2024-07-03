using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;

namespace Clean.Architecture.And.DDD.Template.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IConnectionMultiplexer _multiplexer;

        public WeatherForecastController(ILogger<WeatherForecastController> logger,
            IConnectionMultiplexer multiplexer)
        {
            _logger = logger;
            _multiplexer = multiplexer;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            var redis = _multiplexer.GetDatabase();
            redis.SetAddAsync($"key1", "value1");
            var count = redis.KeyRefCount("key1");
            return null;
        }
    }
}
