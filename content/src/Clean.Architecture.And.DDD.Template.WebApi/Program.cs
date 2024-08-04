using Clean.Architecture.And.DDD.Template.Domian;
using Clean.Architecture.And.DDD.Template.Domian.Customers;
using Clean.Architecture.And.DDD.Template.Domian.Orders;
using Clean.Architecture.And.DDD.Template.Infrastructure.BackgroundTasks;
using Clean.Architecture.And.DDD.Template.Infrastructure.Events;
using Clean.Architecture.And.DDD.Template.Infrastructure.Installers;
using Clean.Architecture.And.DDD.Template.Infrastructure.Persistance.Configuration.Domain.Customers;
using Clean.Architecture.And.DDD.Template.Infrastructure.Persistance.Configuration.Domain.Orders;
using Clean.Architecture.And.DDD.Template.Infrastructure.Persistance.MsSql;
using Clean.Architecture.And.DDD.Template.Infrastructure.Shared;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.InstallEntityFramework();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.InstallApplicationSettings();

var redisConnection = builder.InstallRedis();

builder.Services.AddSingleton<IConnectionMultiplexer>(redisConnection);

builder.InstallTelemetry(builder.Configuration, redisConnection);
builder.InstallMassTransit();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddTransient<IDateTimeProvider, DateTimeProvider>();
builder.Services.AddTransient<IDomainEventDispatcher, DomainEventDispatcher>();
builder.Services.AddHostedService<DomainEventsProcessor>();
builder.Services.AddHostedService<IntegrationEventsProcessor>();
var app = builder.Build();


using (var scope = app.Services.CreateScope())
{
    var appDbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    EntityFrameworkInstaller.SeedDatabase(appDbContext);
};

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

//app.UseAuthorization();

app.MapControllers();

app.Run();
