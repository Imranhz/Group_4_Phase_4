using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace Group4Flight.Models.DataLayer.Repositories
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> GetAll();
        T? GetById(object id);
        IQueryable<T> Find(Expression<Func<T, bool>> predicate);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        void SaveChanges();
    }
}
