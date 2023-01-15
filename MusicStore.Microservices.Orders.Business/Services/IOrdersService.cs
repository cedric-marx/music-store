using MusicStore.Microservices.Orders.Data.Models.Domain;
using MusicStore.Microservices.Orders.Data.Models.Dto;

namespace MusicStore.Microservices.Orders.Business.Services;

public interface IOrdersService
{
    Task<bool> Create(Order order, CancellationToken cancellationToken = default);

    Task<IEnumerable<OrderDto>> ReadAll(CancellationToken cancellationToken = default);
}