using CA.And.DDD.Template.Infrastructure.Installers;
using CA.And.DDD.Template.Infrastructure.Persistance.MsSql;
using Scalar.AspNetCore;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);
builder.InstallAuthentication();
// Add services to the container.
builder.InstallEntityFramework();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.InstallSwagger();
builder.InstallApplicationSettings();

var redisConnection = builder.InstallRedis();
builder.InstallRedisCache();

builder.Services.AddSingleton<IConnectionMultiplexer>(redisConnection);

builder.InstallTelemetry(builder.Configuration, redisConnection);
builder.InstallMassTransit();
builder.InstallDependencyInjectionRegistrations();
builder.Services.AddOpenApi();
builder.InstallCors();

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
