using AutoMapper;
using SampleStack.AutoMapper.Data;
using SampleStack.AutoMapper.Models;

namespace SampleStack.AutoMapper.Services
{
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
}
