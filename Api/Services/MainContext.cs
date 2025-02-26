using Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Services;

public class MainContext(DbContextOptions<MainContext> options) : DbContext(options)
{
    public DbSet<Application> Applications { get; set; }
}