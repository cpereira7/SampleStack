namespace SampleStack.AutoMapper.Services
{
    internal interface IService<out T> where T : class
    {
        Task RetrieveData();
        IReadOnlyList<T> GetAllItems();
        T? GetItem(int id);
    }
}
