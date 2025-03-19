using DotNetEnv;
using Microsoft.OpenApi.Models;
using MovieApi.Services;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

Env.Load();

const string apiAccessToken = "API_ACCESS_TOKEN";
var token = Env.GetString(apiAccessToken)
    ?? throw new InvalidOperationException($"Missing required API token: {apiAccessToken} in .env file.");

builder.Services.AddMemoryCache();
builder.Services.AddHttpClient<IMovieProviderService, MovieProviderService>();
builder.Services.AddScoped<IMovieProviderService, MovieProviderService>();
builder.Services.AddScoped<IMovieAggregatorService, MovieAggregatorService>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("logs/api-log.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

builder.Logging.ClearProviders();
builder.Logging.AddSerilog();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();