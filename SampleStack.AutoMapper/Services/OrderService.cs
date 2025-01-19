using AutoMapper;
using SampleStack.AutoMapper.Data;
using SampleStack.AutoMapper.DTOs;
using SampleStack.AutoMapper.Mapping;
using SampleStack.AutoMapper.Models;

namespace SampleStack.AutoMapper.Services
{

    internal class OrderService(IDataSource<OrderDto> dataSource, IMapService mapper) : BaseService<Order, OrderDto>(dataSource, mapper)
    {
    }

    internal class ProductService(IDataSource<ProductDto> dataSource, IMapService mapper) : BaseService<Product, ProductDto>(dataSource, mapper)
    {
    }

    internal class CustomerService(IDataSource<CustomerDto> dataSource, IMapService mapper) : BaseService<Customer, CustomerDto>(dataSource, mapper)
    {
    }
}
