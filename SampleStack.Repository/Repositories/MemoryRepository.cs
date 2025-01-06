using SampleStack.Repository.Models;

namespace SampleStack.Repository.Repositories
{
    public class MemoryRepository<T> : IRepository<T> where T : IModelBase
    {
        private readonly List<T> _list;

        public MemoryRepository()
        {
            _list = [];
        }

        public void Add(T entity)
        {
            if (GetById(entity.Id) == null)
            {
                _list.Add(entity);
            }
        }

        public void Delete(T entity)
        {
            _list.Remove(entity);
        }

        public void DeleteAll()
        {
            _list.Clear();
        }

        public IReadOnlyCollection<T> GetAll()
        {
            return _list.AsReadOnly();
        }

        public T GetById(int id)
        {
            return _list.SingleOrDefault(x => x.Id == id)!;
        }

        public void Update(T entity)
        {
            var existingEntity = GetById(entity.Id);

            if (existingEntity != null)
            {
                var index = _list.IndexOf(existingEntity);
                _list[index] = entity;
            }
        }
    }
}
