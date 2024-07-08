using Clean.Architecture.And.DDD.Template.Infrastructure.Database.MsSql;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;

namespace Clean.Architecture.And.DDD.Template.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IConnectionMultiplexer _multiplexer;
        private readonly AppDbContext _appDbContext;

        public WeatherForecastController(ILogger<WeatherForecastController> logger,
            IConnectionMultiplexer multiplexer,
            AppDbContext appDbContext)
        {
            _logger = logger;
            _multiplexer = multiplexer;
            _appDbContext = appDbContext;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public async Task<IActionResult> Get()
        {
            var redis = _multiplexer.GetDatabase();
            await redis.SetAddAsync($"key1", DateTime.Now.ToString());
            var count = redis.KeyRefCount("key1");
            var newUser = new Infrastructure.Database.MsSql.Models.User()
            {
                Name = $"Mikolaj-{DateTime.UtcNow}",
                Surname = $"Jankowski-{DateTime.UtcNow}"
            };
            await _appDbContext.Users.AddAsync(newUser);
            _logger.LogInformation($"Inserting a user: {newUser.Name} {newUser.Surname}");
            await _appDbContext.SaveChangesAsync();
            var top5Users = await _appDbContext.Users.FromSqlRaw("select TOP(5)* from dbo.Users").ToListAsync();
            return Ok();
        }
    }
}
