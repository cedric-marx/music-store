using AutoMapper;
using MusicStore.Microservices.Orders.Api.RequestModels;
using MusicStore.Microservices.Orders.Data.Models.Domain;
using MusicStore.Microservices.Orders.Data.Models.Dto;

namespace MusicStore.Microservices.Orders.Api.AutomapperProfiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Order, OrderDto>().ReverseMap();
        CreateMap<CreateOrderRequestModel, Order>().ReverseMap();
    }
}