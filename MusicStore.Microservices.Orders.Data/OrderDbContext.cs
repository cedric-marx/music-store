using Microsoft.EntityFrameworkCore;
using MusicStore.Microservices.Orders.Data.Models.Domain;

namespace MusicStore.Microservices.Orders.Data;

/// <summary>
///     TO MIGRATE THIS DBCONTEXT RUN COMMAND:
///     dotnet ef migrations add MIGRATION_NAME -c ProductDbContext -s ./MusicStore.Microservices.Orders.Api -p
///     ./MusicStore.Microservices.Orders.Data
///     ==================================================
///     UPDATE DATABASE TO LATEST MIGRATION WITH COMMAND:
///     dotnet ef database update -c ProductDbContext -s ./MusicStore.Microservices.Orders.Api -p
///     ./MusicStore.Microservices.Orders.Data
/// </summary>
public class OrderDbContext : DbContext
{
    public OrderDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Order> Orders { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Order>().HasKey(p => p.Id);
        modelBuilder.Entity<Order>().Property(p => p.Id).ValueGeneratedOnAdd();
    }
}