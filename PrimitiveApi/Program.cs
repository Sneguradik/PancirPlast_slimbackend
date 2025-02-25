using System.Net;
using Microsoft.EntityFrameworkCore;
using PrimitiveApi;
using PrimitiveApi.Dto;
using PrimitiveApi.Services;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(opts=>opts.AddDefaultPolicy(policy=>policy
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowCredentials()
    .AllowAnyHeader()));
builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
//builder.Services.AddDbContext<MainContext>(opt=>
    //opt.UseNpgsql(Environment.GetEnvironmentVariable("CONNECTION_STRING")));

builder.Services.Configure<TelegramBotConfig>(conf =>
{
    var botToken = Environment.GetEnvironmentVariable("TELEGRAM_BOT_TOKEN");
    if (botToken is null) throw new NullReferenceException("No telegram bot token found");
    conf.BotToken = botToken;
});
builder.Services.AddSingleton<IBotService, BotService>();
//builder.Services.AddHostedService<TelegramBotWorker>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

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