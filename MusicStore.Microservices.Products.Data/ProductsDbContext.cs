using Microsoft.EntityFrameworkCore;
using MusicStore.Microservices.Products.Data.Models;

namespace MusicStore.Microservices.Products.Data;

/// <summary>
///     TO MIGRATE THIS DBCONTEXT RUN COMMAND:
///     dotnet ef migrations add MIGRATION_NAME -c ProductDbContext -s ./MusicStore.Microservices.Products.Api -p
///     ./MusicStore.Microservices.Products.Data
///     ==================================================
///     UPDATE DATABASE TO LATEST MIGRATION WITH COMMAND:
///     dotnet ef database update -c ProductDbContext -s ./MusicStore.Microservices.Products.Api -p
///     ./MusicStore.Microservices.Products.Data
/// </summary>
public class ProductsDbContext : DbContext
{
    public ProductsDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Product> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Product>().HasKey(p => p.Id);
        modelBuilder.Entity<Product>().Property(p => p.Id).ValueGeneratedOnAdd();
    }
}