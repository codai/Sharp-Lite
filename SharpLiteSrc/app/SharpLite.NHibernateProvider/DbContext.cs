using System;
using NHibernate;
using SharpLite.Domain.DataInterfaces;

namespace SharpLite.NHibernateProvider
{
    public class DbContext : IDbContext
    {
        public DbContext(ISessionFactory sessionFactory) {
            if (sessionFactory == null) throw new ArgumentNullException("sessionFactory may not be null");

            _sessionFactory = sessionFactory;
        }

        public virtual IDisposable BeginTransaction() {
            return _sessionFactory.GetCurrentSession().BeginTransaction();
        }

        /// <summary>
        /// This isn't specific to any one DAO and flushes everything that has been
        /// changed since the last commit.
        /// </summary>
        public virtual void CommitChanges() {
            _sessionFactory.GetCurrentSession().Flush();
        }

        public virtual void CommitTransaction() {
            _sessionFactory.GetCurrentSession().Transaction.Commit();
        }

        public virtual void RollbackTransaction() {
            _sessionFactory.GetCurrentSession().Transaction.Rollback();
        }

        private readonly ISessionFactory _sessionFactory;
    }
}
