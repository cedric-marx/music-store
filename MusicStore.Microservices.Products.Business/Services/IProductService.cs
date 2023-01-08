using MusicStore.Microservices.Products.Business.Models;
using MusicStore.Microservices.Products.Data.Models;

namespace MusicStore.Microservices.Products.Business.Services;

public interface IProductService
{
    Task<bool> Create(Product product, CancellationToken cancellationToken = default);

    Task<IEnumerable<ProductDto>> ReadAll(CancellationToken cancellationToken = default);
}