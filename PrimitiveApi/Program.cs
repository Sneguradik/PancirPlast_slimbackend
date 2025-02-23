using Microsoft.EntityFrameworkCore;
using PrimitiveApi.Services;

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
builder.Services.AddDbContext<MainContext>(opt=>
    opt.UseNpgsql(builder.Configuration.GetConnectionString("MainContext")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();