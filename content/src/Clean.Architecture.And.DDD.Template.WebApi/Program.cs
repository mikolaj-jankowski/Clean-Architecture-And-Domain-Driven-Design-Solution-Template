using Clean.Architecture.And.DDD.Template.Domian.Employees;
using Clean.Architecture.And.DDD.Template.Infrastructure.Database.MsSql;
using Clean.Architecture.And.DDD.Template.Infrastructure.Domain.Employees;
using Clean.Architecture.And.DDD.Template.Infrastructure.Events;
using Clean.Architecture.And.DDD.Template.Infrastructure.Installers;
using Clean.Architecture.And.DDD.Template.Infrastructure.Settings;
using Clean.Architecture.And.DDD.Template.WebApi.Installers;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.InstallApplicationSettings();
builder.InstallEntityFramework();
var redisConnection = builder.InstallRedis();

builder.Services.AddSingleton<IConnectionMultiplexer>(redisConnection);

builder.InstallTelemetry(builder.Configuration, redisConnection);
builder.InstallMassTransit();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IEmployeeRespository, EmployeeRepository>();
builder.Services.AddTransient<IDomainEventDispatcher, DomainEventDispatcher>();

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
