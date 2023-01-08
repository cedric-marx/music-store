using AutoMapper;
using MassTransit;
using MusicStore.Microservices.Orders.Data.Models.Domain;
using MusicStore.Microservices.Orders.Data.Models.Dto;
using MusicStore.Microservices.Orders.Data.Repositories;
using MusicStore.Microservices.Orders.Events.Events;

namespace MusicStore.Microservices.Orders.Business.Services.Implementation;

public class OrderService : IOrderService
{
    private readonly IMapper _mapper;
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly IRepository<Order> _orderRepository;

    public OrderService(IRepository<Order> orderRepository, IMapper mapper, IPublishEndpoint publishEndpoint)
    {
        _orderRepository = orderRepository;
        _mapper = mapper;
        _publishEndpoint = publishEndpoint;
    }

    public async Task<bool> Create(Order order, CancellationToken cancellationToken = default)
    {
        var isSuccess = await _orderRepository.CreateAsync(order, cancellationToken);
        if (isSuccess)
        {
            await _publishEndpoint.Publish(new OrderCreatedEvent(_mapper.Map<OrderDto>(order)), cancellationToken);
        }

        return isSuccess;
    }

    public async Task<IEnumerable<OrderDto>> ReadAll(CancellationToken cancellationToken = default)
    {
        var products = await _orderRepository.ReadAllAsync(cancellationToken);
        return _mapper.Map<IEnumerable<OrderDto>>(products);
    }
}