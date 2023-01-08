using MusicStore.Microservices.Orders.Data.Models.Domain.Base;

namespace MusicStore.Microservices.Orders.Data.Models.Domain;

public class Order : IEntity
{
    public Guid ProductId { get; set; }

    public Guid Id { get; set; }
}