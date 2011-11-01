using System;
using System.Linq;
using NHibernate;
using NHibernate.Linq;
using SharpLite.Domain.DataInterfaces;

namespace SharpLite.NHibernateProvider
{
    public class Repository<T> : RepositoryWithTypedId<T, int>, IRepository<T> where T : class
    {
        public Repository(ISessionFactory sessionFactory) : base(sessionFactory) { }
    }

    public class RepositoryWithTypedId<T, TId> : IRepositoryWithTypedId<T, TId> where T : class
    {
        public RepositoryWithTypedId(ISessionFactory sessionFactory) {
            if (sessionFactory == null) throw new ArgumentNullException("sessionFactory may not be null");

            _sessionFactory = sessionFactory;
        }

        protected virtual ISession Session {
            get {
                return _sessionFactory.GetCurrentSession();
            }
        }

        public virtual IDbContext DbContext {
            get {
                return new DbContext(_sessionFactory);
            }
        }

        public virtual T Get(TId id) {
            return Session.Get<T>(id);
        }

        public virtual IQueryable<T> GetAll() {
            return Session.Query<T>();
        }

        public virtual T SaveOrUpdate(T entity) {
            Session.SaveOrUpdate(entity);
            return entity;
        }

        /// <summary>
        /// This deletes the object and commits the deletion immediately.  We don't want to delay deletion
        /// until a transaction commits, as it may throw a foreign key constraint exception which we could
        /// likely handle and inform the user about.  Accordingly, this tries to delete right away; if there
        /// is a foreign key constraint preventing the deletion, an exception will be thrown.
        /// </summary>
        public virtual void Delete(T entity) {
            Session.Delete(entity);
            Session.Flush();
        }

        private readonly ISessionFactory _sessionFactory;
    }
}
