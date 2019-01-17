using RealProperty.Data.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace RealProperty.Data.Repositories.Concrete
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly DbSet<T> _entities;

        public Repository(DataContext context)
        {
            Context = context;
            _entities = Context.Set<T>();
        }

        public DataContext Context { get; }

        public IQueryable<T> Get()
        {
            return _entities.AsQueryable();
        }

        public IQueryable<T> GetReadonly()
        {
            return _entities.AsNoTracking().AsQueryable();
        }

        public void Create(T entity)
        {
            _entities.Add(entity);
        }

        public void Update(T entity)
        {
            _entities.Update(entity);
        }

        public void Delete(T entity)
        {
            _entities.Remove(entity);
        }

        public void SaveChanges()
        {
            Context.SaveChanges();
        }

        public void DetachAll()
        {
            foreach (var dbEntityEntry in Context.ChangeTracker.Entries())
            {
                if (dbEntityEntry.Entity != null)
                    dbEntityEntry.State = EntityState.Detached;
            }
        }
    }
}
