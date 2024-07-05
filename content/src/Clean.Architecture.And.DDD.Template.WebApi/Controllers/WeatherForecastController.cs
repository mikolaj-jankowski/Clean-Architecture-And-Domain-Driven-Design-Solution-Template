using Clean.Architecture.And.DDD.Template.Infrastructure.Settings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using StackExchange.Redis;

namespace Clean.Architecture.And.DDD.Template.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IConnectionMultiplexer _multiplexer;
        private readonly IOptions<Redis> _redisSettings;

        public WeatherForecastController(ILogger<WeatherForecastController> logger,
            IConnectionMultiplexer multiplexer,
            IOptions<Redis> redisSettings)
        {
            _logger = logger;
            _multiplexer = multiplexer;
            _redisSettings = redisSettings;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IActionResult Get()
        {
            var redis = _multiplexer.GetDatabase();
            redis.SetAddAsync($"key1", DateTime.Now.ToString());
            var count = redis.KeyRefCount("key1");
            return Ok();
        }
    }
}
