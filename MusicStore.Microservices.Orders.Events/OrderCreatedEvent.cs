using MusicStore.Microservices.Orders.Data.Models.Dto;

namespace MusicStore.Microservices.Orders.Events.Events;

public record OrderCreatedEvent(OrderDto OrderDto);