using Clean.Architecture.And.DDD.Template.Application.Customer.CreateCustomer;
using Clean.Architecture.And.DDD.Template.Infrastructure.Persistance.MsSql;
using MassTransit.Mediator;
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
        private readonly IMediator _mediator;

        public WeatherForecastController(ILogger<WeatherForecastController> logger,
            IConnectionMultiplexer multiplexer,
            AppDbContext appDbContext,
            IMediator mediator)
        {
            _logger = logger;
            _multiplexer = multiplexer;
            _appDbContext = appDbContext;
            _mediator = mediator;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public async Task<IActionResult> Get()
        {
            await _mediator.Send<CreateCustomerCommand>(new CreateCustomerCommand("Filip", "Jankowski", DateTime.Now.AddYears(-30)));
            var redis = _multiplexer.GetDatabase();
            await redis.SetAddAsync($"key1", DateTime.Now.ToString());
            var count = redis.KeyRefCount("key1");
           // var top5Users = await _appDbContext.Employees.FromSqlRaw("select TOP(5)* from dbo.Employees").ToListAsync();
            
            return Ok();
        }
    }
}
