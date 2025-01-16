using AutoMapper;
using SampleStack.AutoMapper.DTOs;
using SampleStack.AutoMapper.Models;

namespace SampleStack.AutoMapper
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

    internal class ProductResolver : IValueResolver<OrderItemDto, OrderItem, Product>
    {
        private readonly ProductService _productsService;
        public ProductResolver(ProductService productsService)
        {
            _productsService = productsService;
        }
        public Product Resolve(OrderItemDto source, OrderItem destination, Product destMember, ResolutionContext context)
        {
            return _productsService.GetItem(source.ProductId);
        }
    }

    internal class CustomerResolver : IValueResolver<OrderDto, Order, Customer>
    {
        private readonly CustomerService _customerService;
        public CustomerResolver(CustomerService customerService)
        {
            _customerService = customerService;
        }
        public Customer Resolve(OrderDto source, Order destination, Customer destMember, ResolutionContext context)
        {
            return _customerService.GetItem(source.CustomerId);
        }
    }
}
