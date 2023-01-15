using MassTransit;
using MusicStore.Microservices.Orders.Events.Events;
using MusicStore.Microservices.Products.Data.Models;
using MusicStore.Microservices.Products.Data.Repositories;

namespace MusicStore.Microservices.Products.Api.Handlers;

public class OrderCreatedEventHandler : IConsumer<OrderCreatedEvent>
{
    private readonly IRepository<Product> _productRepository;

    public OrderCreatedEventHandler(IRepository<Product> productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task Consume(ConsumeContext<OrderCreatedEvent> context)
    {
        await UpdateProductStock(context.Message.OrderDto.ProductId);
    }

    private async Task UpdateProductStock(Guid productId)
    {
        var product = await _productRepository.ReadAsync(productId);
        product.Stock--;
        await _productRepository.UpdateAsync(product);
    }
}