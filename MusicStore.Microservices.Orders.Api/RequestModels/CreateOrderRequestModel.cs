using FluentValidation;

namespace MusicStore.Microservices.Orders.Api.RequestModels;

public record CreateOrderRequestModel(Guid ProductId);

public class OrderRequestModelValidator : AbstractValidator<CreateOrderRequestModel>
{
    public OrderRequestModelValidator()
    {
        RuleFor(x => x.ProductId).NotNull();
    }
}