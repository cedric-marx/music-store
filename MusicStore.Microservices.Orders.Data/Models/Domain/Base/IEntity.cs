namespace MusicStore.Microservices.Orders.Data.Models.Domain.Base;

public interface IEntity
{
    Guid Id { get; set; }
}