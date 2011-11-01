using System;
using System.Data;
using SharpLite.Domain.DataInterfaces;

namespace SharpLite.EntityFrameworkProvider
{
    public class DbContext : IDbContext
    {
        public DbContext(System.Data.Entity.DbContext dbContext) {
            if (dbContext == null) throw new ArgumentNullException("dbContext may not be null");

            _dbContext = dbContext;
        }

        public virtual IDisposable BeginTransaction() {
            _transaction = _dbContext.Database.Connection.BeginTransaction();
            return _transaction;
        }

        /// <summary>
        /// This isn't specific to any one DAO and flushes everything that has been
        /// changed since the last commit.
        /// </summary>
        public virtual void CommitChanges() {
            _dbContext.SaveChanges();
        }

        public virtual void CommitTransaction() {
            if (_transaction != null)
                _transaction.Commit();
        }

        public virtual void RollbackTransaction() {
            if (_transaction != null)
                _transaction.Rollback();
        }

        private static IDbTransaction _transaction;
        private readonly System.Data.Entity.DbContext _dbContext;
    }
}
