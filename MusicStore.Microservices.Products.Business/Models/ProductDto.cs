namespace MusicStore.Microservices.Products.Business.Models;

public record ProductDto(Guid Id, string Name, string Description, double Price, string ImageUrl, long Stock = 100);