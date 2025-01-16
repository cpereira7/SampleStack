using AutoMapper;
using SampleStack.AutoMapper.Data;
using SampleStack.AutoMapper.DTOs;
using SampleStack.AutoMapper.Models;

namespace SampleStack.AutoMapper
{
    internal interface IService<out T> where T : class
    {
        Task RetrieveData();
        IReadOnlyList<T> GetAllItems();
        T? GetItem(int id);
    }

    internal abstract class BaseService<TDomain, TDto> : IService<TDomain> 
        where TDomain : BaseModel
        where TDto : class
    {
        private readonly IDataSource<TDto> _dataSource;
        private readonly IMapper _mapper;
        private readonly List<TDomain> _data;

        protected BaseService(IDataSource<TDto> dataSource, IMapper mapper)
        {
            _dataSource = dataSource;
            _mapper = mapper;
            _data = [];
        }

        public IReadOnlyList<TDomain> GetAllItems() => _data.AsReadOnly();

        public TDomain? GetItem(int id) => _data.FirstOrDefault(x => x.Id == id);

        public async Task RetrieveData()
        {
            var dtos = await _dataSource.GetAllData();
            var domainObjects = dtos.Select(ConvertToDomain).ToList();

            _data.Clear();
            _data.AddRange(domainObjects);
        }

        protected virtual TDomain ConvertToDomain(TDto dto)
        {
            return _mapper.Map<TDomain>(dto);
        }
    }

    internal class OrderService : BaseService<Order, OrderDto>
    {
        public OrderService(IDataSource<OrderDto> dataSource, IMapper mapper) : base(dataSource, mapper)
        {
        }
    }

    internal class ProductService : BaseService<Product, ProductDto>
    {
        public ProductService(IDataSource<ProductDto> dataSource, IMapper mapper) : base(dataSource, mapper)
        {
        }
    }

    internal class CustomerService : BaseService<Customer, CustomerDto>
    {
        public CustomerService(IDataSource<CustomerDto> dataSource, IMapper mapper) : base(dataSource, mapper)
        {
        }
    }
}
