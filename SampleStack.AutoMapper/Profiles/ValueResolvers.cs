using AutoMapper;
using SampleStack.AutoMapper.DTOs;
using SampleStack.AutoMapper.Models;
using SampleStack.AutoMapper.Services;

namespace SampleStack.AutoMapper.Profiles
{
    internal abstract class BaseResolver<TSource, TDestination, TMember, TService> : IValueResolver<TSource, TDestination, TMember>
        where TMember : class
        where TService : IService<TMember>
    {
        private readonly TService _service;

        protected BaseResolver(TService service)
        {
            _service = service;
        }

        public TMember Resolve(TSource source, TDestination destination, TMember destMember, ResolutionContext context)
        {
            var id = GetIdFromSource(source);

            return _service.GetItem(id) ?? throw new NullReferenceException($"Unable to resolve {typeof(TMember).Name} with the provided ID ({id}).");
        }

        protected abstract int GetIdFromSource(TSource source);
    }

    internal class ProductResolver(ProductService service) : BaseResolver<OrderItemDto, OrderItem, Product, ProductService>(service)
    {
        protected override int GetIdFromSource(OrderItemDto source)
        {
            return source.ProductId;
        }
    }

    internal class CustomerResolver(CustomerService customerService) : BaseResolver<OrderDto, Order, Customer, CustomerService>(customerService)
    {
        protected override int GetIdFromSource(OrderDto source)
        {
            return source.CustomerId;
        }
    }
}
