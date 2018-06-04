using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Surging.Identity.Database
{
    public interface IRepository
    {
        IdentityContext DbContext { get; }

        Task<int> SaveAsync();
        void Dispose();

        Task<T> GetByIdAsync<T>(object id) where T : class;

        IQueryable<T> Table<T>(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = "") where T : class;
        IQueryable<T> Table<T>(Expression<Func<T, bool>> filter, string includeProperties) where T : class;
        IQueryable<T> Table<T>(string includeProperties) where T : class;

        void Insert<T>(T entity) where T : class;
        void Delete<T>(object id) where T : class;
        void Delete<T>(T entityToDelete) where T : class;
        void Update<T>(T entityToUpdate) where T : class;

        void BeginTrans();
        void Rollback();
        void Commit();
        bool InTransaction();
    }
}
