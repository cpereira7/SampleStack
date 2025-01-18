namespace SampleStack.AutoMapper.Data
{
    internal interface IDataSource<T> where T : class
    {
        Task<IEnumerable<T>> GetAllData();
        Task<T?> GetById(int id);
    }
}
