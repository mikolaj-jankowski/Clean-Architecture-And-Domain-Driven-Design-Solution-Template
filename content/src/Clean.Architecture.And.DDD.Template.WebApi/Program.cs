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
builder.I
var redisConnection = ConnectionMultiplexer.Connect(builder.Configuration.GetSection(nameof(AppSettings)).Get<AppSettings>()!.Redis.Url);
builder.Services.AddSingleton<IConnectionMultiplexer>(redisConnection);

builder.InstallTelemetry(builder.Configuration, redisConnection);

var app = builder.Build();

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
