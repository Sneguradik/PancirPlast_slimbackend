using Microsoft.EntityFrameworkCore;
using PrimitiveApi.Models;

namespace PrimitiveApi.Services;

public class MainContext(DbContextOptions<MainContext> options) : DbContext(options)
{
    public DbSet<Application> Applications { get; set; }
}