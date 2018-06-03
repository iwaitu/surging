using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Surging.Identity.Database
{
    public class IdentityRepository : IRepository,IDisposable
    {
        private Lazy<IdentityContext> _lazyContext;
        private IDbContextTransaction _transaction;

        public IdentityRepository(IdentityContext context)
        {
            _lazyContext = new Lazy<IdentityContext>(() =>
            {
                var db = context;
                return db;
            }, LazyThreadSafetyMode.ExecutionAndPublication);
        }

        public IdentityContext DbContext
        {
            get
            {
                return _lazyContext.Value;
            }
        }

        public void BeginTrans()
        {
            _transaction = DbContext?.Database?.BeginTransaction();
        }

        public void Commit()
        {
            DbContext?.Database?.CommitTransaction();
        }



        public virtual void Delete<T>(object id) where T : class
        {
            T entityToDelete = DbContext.Set<T>().Find(id);
            Delete(entityToDelete);
        }

        public void Delete<T>(T entityToDelete) where T : class
        {
            if (entityToDelete == null)
            {
                return;
            }
            if (DbContext.Entry(entityToDelete).State == EntityState.Detached)
            {
                DbContext.Set<T>().Attach(entityToDelete);
            }
            DbContext.Set<T>().Remove(entityToDelete);
        }

        public void Dispose()
        {
            DbContext.Dispose();
        }


        public async Task<T> GetByIdAsync<T>(object id) where T : class
        {
            return await DbContext.Set<T>().FindAsync(id);
        }


        public virtual void Insert<T>(T entity) where T : class
        {
            DbContext.Set<T>().Add(entity);
        }

        public bool InTransaction()
        {
            return _transaction != null;
        }

        public void Rollback()
        {
            DbContext?.Database.RollbackTransaction();
        }


        public async Task<int> SaveAsync()
        {
            return await DbContext.SaveChangesAsync();
        }

        public IQueryable<T> Table<T>(
            Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "") where T : class
        {
            IQueryable<T> query = DbContext.Set<T>();
            if (filter != null)
            {
                query = query.Where(filter);
            }

            query = includeProperties.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Aggregate(query, (current, includeProperty) => current.Include(includeProperty));

            return orderBy != null ? orderBy(query) : query;
        }

        public IQueryable<T> Table<T>(Expression<Func<T, bool>> filter, string includeProperties) where T : class
        {
            return Table(filter, null, includeProperties);
        }

        public IQueryable<T> Table<T>(string includeProperties) where T : class
        {
            return Table<T>(null, null, includeProperties);
        }

        public void Update<T>(T entityToUpdate) where T : class
        {
            throw new NotImplementedException();
        }
    }
}
