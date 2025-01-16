using AutoMapper;
using SampleStack.AutoMapper.DTOs;
using SampleStack.AutoMapper.Models;
using SampleStack.AutoMapper.Services;

namespace SampleStack.AutoMapper.Profiles
{
    internal class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Customer, CustomerDto>().ReverseMap();
            CreateMap<Product, ProductDto>().ReverseMap();

            CreateMap<OrderItem, OrderItemDto>().ReverseMap()
                .ForMember(dest => dest.Product, opt => opt.MapFrom<ProductResolver>());

            CreateMap<Order, OrderDto>().ReverseMap()
                .ForMember(dest => dest.Customer, opt => opt.MapFrom<CustomerResolver>());
        }
    }
}
