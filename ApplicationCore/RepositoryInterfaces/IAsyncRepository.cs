using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ApplicationCore.RepositoryInterfaces
{
    // base interface that all your repository interfaces will inherit
    public interface IAsyncRepository<T> where T : class
    {
        // CRUD
        // get by id
        Task<T> GetById(int id);
        Task<IEnumerable<T>> GetAll();
        Task<IEnumerable<T>> Get(Expression<Func<T, bool>> predicate);
        Task<int> GetCount(Expression<Func<T, bool>> predicate);
        Task<T> Add(T entity);
        Task<T> Update(T entity);
        Task Delete(T entity);
    }
}