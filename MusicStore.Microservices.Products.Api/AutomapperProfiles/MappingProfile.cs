using AutoMapper;
using MusicStore.Microservices.Products.Api.RequestModels;
using MusicStore.Microservices.Products.Business.Models;
using MusicStore.Microservices.Products.Data.Models;

namespace MusicStore.Microservices.Products.Api.AutomapperProfiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Product, ProductDto>().ReverseMap();
        CreateMap<CreateProductRequestModel, Product>();
    }
}