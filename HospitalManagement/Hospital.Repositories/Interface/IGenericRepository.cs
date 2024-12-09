using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace Hospital.Repositories.Interface
{
    public interface IGenericRepository<T> : IDisposable

    {


         IEnumerable<T> GetAll(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderby = null,
            string includeProperties = ""); 
        T GetById(object id);

        Task<T>  GetByIdAsync(object id);


        void Add(T entity);

        Task<T> AddAsync( T entity);

        void Update(T entity);

        Task<T> UpdateAsync(T entity);

        void Delete(T entity);

        Task<T> DeleteAsync(T entity);

       
    }
}
