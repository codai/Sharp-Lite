using System;
using System.Linq;
using SharpLite.Domain;
using SharpLite.Domain.DataInterfaces;

namespace SharpLite.EntityFrameworkProvider
{
    public class Repository<T> : RepositoryWithTypedId<T, int>, IRepository<T> where T : class, IEntityWithTypedId<int>
    {
        public Repository(System.Data.Entity.DbContext dbContext) : base(dbContext) { }
    }

    public class RepositoryWithTypedId<T, TId> : IRepositoryWithTypedId<T, TId> where T : class, IEntityWithTypedId<TId>
    {
        public RepositoryWithTypedId(System.Data.Entity.DbContext dbContext) {
            if (dbContext == null) throw new ArgumentNullException("dbContext may not be null");

            _dbContext = dbContext;
        }

        public virtual IDbContext DbContext {
            get {
                return new DbContext(_dbContext);
            }
        }

        public virtual T Get(TId id) {
            return _dbContext.Set<T>().Single(entity => id.Equals(entity.Id));
        }

        public virtual IQueryable<T> GetAll() {
            return _dbContext.Set<T>();
        }

        public virtual T SaveOrUpdate(T entity) {
            if (entity == null)
                return null;

            if (entity.IsTransient())
                _dbContext.Set<T>().Add(entity);

            return entity;
        }

        /// <summary>
        /// This deletes the object and commits the deletion immediately.  We don't want to delay deletion
        /// until a transaction commits, as it may throw a foreign key constraint exception which we could
        /// likely handle and inform the user about.  Accordingly, this tries to delete right away; if there
        /// is a foreign key constraint preventing the deletion, an exception will be thrown.
        /// </summary>
        public virtual void Delete(T entity) {
            _dbContext.Set<T>().Remove(entity);
            _dbContext.SaveChanges();
        }

        private readonly System.Data.Entity.DbContext _dbContext;
    }
}
