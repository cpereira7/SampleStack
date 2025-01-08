using Microsoft.EntityFrameworkCore;
using SampleStack.Repository.Models;

namespace SampleStack.Repository.Repositories
{
    public class EfRepository<T> : IRepository<T> where T : class, IModelBase
    {
        private readonly DbContext _dbContext;
        private readonly DbSet<T> _dbSet;

        public EfRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<T>();
        }

        public void Add(T entity)
        {
            if (_dbSet.Find(entity.Id) == null)
            {
                _dbSet.Add(entity);
                _dbContext.SaveChanges();
            }
        }

        public void Delete(T entity)
        {
            if (_dbSet.Find(entity.Id) != null)
            {
                _dbSet.Remove(entity);
                _dbContext.SaveChanges();
            }
        }

        public void DeleteAll()
        {
            _dbSet.RemoveRange(_dbSet);
            _dbContext.SaveChanges();
        }

        public IReadOnlyCollection<T> GetAll()
        {
            return [.. _dbSet];
        }

        public T GetById(int id)
        {
            return _dbSet.Find(id)!;
        }

        public void Update(T entity)
        {
            var trackedEntity = _dbContext.Set<T>().Local.SingleOrDefault(e => e.Id == entity.Id);
            if (trackedEntity != null)
            {
                _dbContext.Entry(trackedEntity).CurrentValues.SetValues(entity);
            }
            else
            {
                _dbContext.Attach(entity);
                _dbContext.Entry(entity).State = EntityState.Modified;
            }

            _dbContext.SaveChanges();
        }
    }

}
