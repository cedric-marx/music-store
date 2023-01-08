using MusicStore.Microservices.Products.Data.Models.Base;

namespace MusicStore.Microservices.Products.Data.Models;

public class Product : IEntity
{
    public string Name { get; set; }

    public string Description { get; set; }

    public double Price { get; set; }

    public string ImageUrl { get; set; }

    public long Stock { get; set; } = 100;
    public Guid Id { get; set; }
}