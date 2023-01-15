using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MusicStore.Microservices.Products.Api.RequestModels;
using MusicStore.Microservices.Products.Business.Models;
using MusicStore.Microservices.Products.Business.Services;
using MusicStore.Microservices.Products.Data.Models;

namespace MusicStore.Microservices.Products.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IProductsService _productsService;

    public ProductsController(IProductsService productsService, IMapper mapper)
    {
        _productsService = productsService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IEnumerable<ProductDto>> ReadAll()
    {
        return await _productsService.ReadAll();
    }

    [HttpPost]
    public async Task<bool> Create(CreateProductRequestModel requestModel)
    {
        var product = _mapper.Map<Product>(requestModel);
        return await _productsService.Create(product);
    }
}