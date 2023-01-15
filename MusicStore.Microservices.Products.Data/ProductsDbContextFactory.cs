using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace MusicStore.Microservices.Products.Data;

public class ProductsDbContextFactory : IDesignTimeDbContextFactory<ProductsDbContext>
{
    public ProductsDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ProductsDbContext>();

        //NOTE: This is only used for development purpose else you can't run a migration.
        optionsBuilder.UseNpgsql(
            "Host=localhost;Port=5432;Database=musicstore_products;User Id=postgres;Password=ILikeMyPassword1#@");

        return new ProductsDbContext(optionsBuilder.Options);
    }
}