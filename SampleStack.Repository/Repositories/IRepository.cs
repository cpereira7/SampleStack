using SampleStack.Repository.Models;

namespace SampleStack.Repository.Repositories
{
    public interface IRepository<T> where T : IModelBase
    {
        T GetById(int id);
        IReadOnlyCollection<T> GetAll();
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        void DeleteAll();
    }
}
