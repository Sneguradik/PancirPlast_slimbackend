using Api.Dto;
using Api.Services;
using Microsoft.EntityFrameworkCore;
using PrimitiveApi;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(opts=>opts.AddDefaultPolicy(policy=>policy
    .WithOrigins(builder.Configuration.GetSection("TrustedOrigins").Get<string[]>()!) 
    .AllowAnyHeader()
    .AllowCredentials()
    .AllowAnyMethod()
    .SetPreflightMaxAge(TimeSpan.FromMinutes(10))));

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.Configure<TelegramBotConfig>(conf =>
{
    var botToken = Environment.GetEnvironmentVariable("TELEGRAM_BOT_TOKEN");
    if (botToken is null) throw new NullReferenceException("No telegram bot token found");
    conf.BotToken = botToken;
});
builder.Services.AddSingleton<IBotService, BotService>();
builder.Services.AddHostedService<TelegramBotWorker>();
builder.Services.AddDbContext<MainContext>(opt =>
    opt.UseNpgsql(Environment.GetEnvironmentVariable("CONNECTION_STRING")));



var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var service = scope.ServiceProvider.GetRequiredService<MainContext>();
    service.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.MapScalarApiReference(options =>
{
    options.WithTheme(ScalarTheme.Mars)
        .WithDarkMode(true)
        .WithDefaultHttpClient(ScalarTarget.CSharp, ScalarClient.HttpClient)
        .WithDarkModeToggle(false);
});

app.Run();