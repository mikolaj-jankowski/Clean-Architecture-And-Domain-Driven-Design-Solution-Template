using CA.And.DDD.Template.Infrastructure.Installers;
using CA.And.DDD.Template.Infrastructure.Persistance.MsSql;
using Scalar.AspNetCore;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;
services.InstallAuthentication(configuration);

// Add services to the container.
services.InstallEntityFramework(configuration);
services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
services.AddEndpointsApiExplorer();
services.InstallSwagger();
services.InstallApplicationSettings(configuration);

var redisConnection = services.InstallRedis(configuration);
services.InstallRedisCache(configuration);

services.AddSingleton<IConnectionMultiplexer>(redisConnection);
services.InstallTelemetry(configuration, redisConnection, builder.Logging);
services.InstallMassTransit(configuration);
services.InstallDependencyInjectionRegistrations(configuration);
services.AddOpenApi();
services.InstallCors(configuration);

var app = builder.Build();


using (var scope = app.Services.CreateScope())
{
    var appDbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    EntityFrameworkInstaller.SeedDatabase(appDbContext);
};


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseCors(CorsInstaller.DefaultCorsPolicyName);
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.UseExceptionHandler();

app.Run();
