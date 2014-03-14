using System;
using System.Data;
using SharpLite.Domain.DataInterfaces;
using System.Data.Entity;

namespace SharpLite.EntityFrameworkProvider
{
    public class DbContext : IDbContext
    {
        public System.Data.Entity.DbContext Context
        {
            get { return _dbContext; }
        }

        public DbContextTransaction Transaction 
        {
            get { return _transaction; }
        }
        public DbContext(System.Data.Entity.DbContext dbContext) 
        {
            if (dbContext == null) throw new ArgumentNullException("dbContext may not be null");

            _dbContext = dbContext;
        }

        public virtual IDisposable BeginTransaction() 
        {
            _transaction = _dbContext.Database.BeginTransaction();
            return _transaction;
        }

        /// <summary>
        /// This isn't specific to any one DAO and flushes everything that has been
        /// changed since the last commit.
        /// </summary>
        public virtual void CommitChanges() 
        {
            _dbContext.SaveChanges();
        }

        public virtual void CommitTransaction() 
        {
            if (_transaction != null)
            { 
                _transaction.Commit();
            }
        }

        public virtual void RollbackTransaction() 
        {
            if (_transaction != null)
            { 
                _transaction.Rollback();
            }
        }

        public virtual void Close()
        {
            if (_dbContext.Database.Connection.State != ConnectionState.Closed)
            {
                _dbContext.Database.Connection.Close();
            }
        }

        private DbContextTransaction _transaction;
        private System.Data.Entity.DbContext _dbContext;
    }
}
