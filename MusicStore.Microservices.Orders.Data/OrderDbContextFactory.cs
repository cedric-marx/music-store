using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace MusicStore.Microservices.Orders.Data;

public class OrderDbContextFactory : IDesignTimeDbContextFactory<OrderDbContext>
{
    public OrderDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<OrderDbContext>();

        //NOTE: This is only used for development purpose else you can't run a migration.
        optionsBuilder.UseNpgsql(
            "Host=localhost;Port=5432;Database=musicstore_orders;User Id=postgres;Password=IWannaBeARockstar");

        return new OrderDbContext(optionsBuilder.Options);
    }
}