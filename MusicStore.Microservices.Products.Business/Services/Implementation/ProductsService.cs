using AutoMapper;
using MusicStore.Microservices.Products.Business.Models;
using MusicStore.Microservices.Products.Data.Models;
using MusicStore.Microservices.Products.Data.Repositories;

namespace MusicStore.Microservices.Products.Business.Services.Implementation;

public class ProductsService : IProductsService
{
    private readonly IMapper _mapper;
    private readonly IRepository<Product> _productRepository;

    public ProductsService(IRepository<Product> productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<bool> Create(Product product, CancellationToken cancellationToken = default)
    {
        return await _productRepository.CreateAsync(product, cancellationToken);
    }

    public async Task<IEnumerable<ProductDto>> ReadAll(CancellationToken cancellationToken = default)
    {
        var products = await _productRepository.ReadAllAsync(cancellationToken);
        return _mapper.Map<IEnumerable<ProductDto>>(products);
    }
}