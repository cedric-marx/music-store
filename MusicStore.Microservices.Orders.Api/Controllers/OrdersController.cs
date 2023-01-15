using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MusicStore.Microservices.Orders.Api.RequestModels;
using MusicStore.Microservices.Orders.Business.Services;
using MusicStore.Microservices.Orders.Data.Models.Domain;
using MusicStore.Microservices.Orders.Data.Models.Dto;

namespace MusicStore.Microservices.Orders.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IOrdersService _ordersService;

    public OrdersController(IOrdersService ordersService, IMapper mapper)
    {
        _ordersService = ordersService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IEnumerable<OrderDto>> ReadAll()
    {
        return await _ordersService.ReadAll();
    }

    [HttpPost]
    public async Task<bool> Create(CreateOrderRequestModel requestModel)
    {
        var product = _mapper.Map<Order>(requestModel);
        return await _ordersService.Create(product);
    }
}