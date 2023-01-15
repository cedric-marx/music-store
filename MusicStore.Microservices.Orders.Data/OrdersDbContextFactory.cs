using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace MusicStore.Microservices.Orders.Data;

public class OrdersDbContextFactory : IDesignTimeDbContextFactory<OrdersDbContext>
{
    public OrdersDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<OrdersDbContext>();

        //NOTE: This is only used for development purpose else you can't run a migration.
        optionsBuilder.UseNpgsql(
            "Host=localhost;Port=5432;Database=musicstore_orders;User Id=postgres;Password=ILikeMyPassword1#@");

        return new OrdersDbContext(optionsBuilder.Options);
    }
}