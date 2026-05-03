using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace Group4Flight.Models.DataLayer.Repositories
{
    public class EfRepository<T> : IRepository<T> where T : class
    {
        private readonly FlightContext _context;
        private readonly DbSet<T> _dbSet;

        public EfRepository(FlightContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public IQueryable<T> GetAll() => _dbSet;

        public T? GetById(object id) => _dbSet.Find(id);

        public IQueryable<T> Find(Expression<Func<T, bool>> predicate) => _dbSet.Where(predicate);

        public void Add(T entity) => _dbSet.Add(entity);

        public void Update(T entity) => _dbSet.Update(entity);

        public void Delete(T entity) => _dbSet.Remove(entity);

        public void SaveChanges() => _context.SaveChanges();
    }
}
