using DotNetEnv;
using Serilog;
using MovieApi.Services;

var builder = WebApplication.CreateBuilder(args);

Env.Load();

const string apiAccessToken = "API_ACCESS_TOKEN";
var token = Env.GetString(apiAccessToken)
    ?? throw new InvalidOperationException($"Missing required API token: {apiAccessToken} in .env file.");


builder.Services.AddHttpClient<IMovieService, MovieService>(client =>
{
    if (!string.IsNullOrEmpty(token))
    {
        client.DefaultRequestHeaders.Add("x-access-token", token);
    }

    client.DefaultRequestHeaders.Add("Accept", "application/json");
});

builder.Services.AddScoped<IMovieService, MovieService>();

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
