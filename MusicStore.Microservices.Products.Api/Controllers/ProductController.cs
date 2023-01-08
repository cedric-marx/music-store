using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MusicStore.Microservices.Products.Api.RequestModels;
using MusicStore.Microservices.Products.Business.Models;
using MusicStore.Microservices.Products.Business.Services;
using MusicStore.Microservices.Products.Data.Models;

namespace MusicStore.Microservices.Products.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IProductService _productService;

    public ProductController(IProductService productService, IMapper mapper)
    {
        _productService = productService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IEnumerable<ProductDto>> ReadAll()
    {
        return await _productService.ReadAll();
    }

    [HttpPost]
    public async Task<bool> Create(CreateProductRequestModel requestModel)
    {
        var product = _mapper.Map<Product>(requestModel);
        return await _productService.Create(product);
    }
}