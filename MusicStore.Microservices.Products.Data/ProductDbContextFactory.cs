using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace MusicStore.Microservices.Products.Data;

public class ProductDbContextFactory : IDesignTimeDbContextFactory<ProductDbContext>
{
    public ProductDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ProductDbContext>();

        //NOTE: This is only used for development purpose else you can't run a migration.
        optionsBuilder.UseNpgsql(
            "Host=localhost;Port=5432;Database=musicstore_products;User Id=postgres;Password=IWannaBeARockstar");

        return new ProductDbContext(optionsBuilder.Options);
    }
}