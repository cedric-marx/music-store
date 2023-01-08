using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MusicStore.Microservices.Orders.Api.RequestModels;
using MusicStore.Microservices.Orders.Business.Services;
using MusicStore.Microservices.Orders.Data.Models.Domain;
using MusicStore.Microservices.Orders.Data.Models.Dto;

namespace MusicStore.Microservices.Orders.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class OrderController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IOrderService _orderService;

    public OrderController(IOrderService orderService, IMapper mapper)
    {
        _orderService = orderService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IEnumerable<OrderDto>> ReadAll()
    {
        return await _orderService.ReadAll();
    }

    [HttpPost]
    public async Task<bool> Create(CreateOrderRequestModel requestModel)
    {
        var product = _mapper.Map<Order>(requestModel);
        return await _orderService.Create(product);
    }
}