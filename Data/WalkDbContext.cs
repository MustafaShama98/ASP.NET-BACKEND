using dotnet.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace dotnet.Data;

public class WalkDbContext : DbContext
{
    public WalkDbContext(DbContextOptions<WalkDbContext> options) : base(options)
    {
    }
    
    public DbSet<Difficulty> Difficulties { get; set; }
    public DbSet<Region> Regions { get; set; }
    public DbSet<Walk> Walks { get; set; }
}