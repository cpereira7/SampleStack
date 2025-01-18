namespace SampleStack.AutoMapper.Data
{
    internal abstract class BaseDataSource<T> : IDataSource<T> where T : class
    {
        protected List<T> Data { get; } = [];
        public async Task<IEnumerable<T>> GetAllData()
        {
            return await Task.FromResult(Data.AsEnumerable());
        }

        public async Task<T?> GetById(int id)
        {
            var item = Data.FirstOrDefault(item => (int)item?.GetType().GetProperty("Id")?.GetValue(item)! == id);

            return await Task.FromResult(item!);
        }
    }
}
