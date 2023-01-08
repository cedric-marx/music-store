using FluentValidation;

namespace MusicStore.Microservices.Products.Api.RequestModels;

public record CreateProductRequestModel(string Name, string Description, double Price, string ImageUrl, long Stock);

public class ProductRequestModelValidator : AbstractValidator<CreateProductRequestModel>
{
    public ProductRequestModelValidator()
    {
        RuleFor(x => x.Name).NotNull();
        RuleFor(x => x.Description).NotNull();
        RuleFor(x => x.Price).NotNull();
    }
}