using System;
using System.Web;
using NHibernate;
using System.Collections.Generic;
// This is needed for the DependencyResolver...wish they would've just used Common Service Locator!
using System.Web.Mvc;
using System.Linq;

namespace SharpLite.NHibernateProvider.Web
{
    /// <summary>
    /// Taken from http://nhforge.org/blogs/nhibernate/archive/2011/03/03/effective-nhibernate-session-management-for-web-apps.aspx
    /// </summary>
    public class SessionPerRequestModule : IHttpModule
    {
        public void Init(HttpApplication context) {
            context.BeginRequest += ContextBeginRequest;
            context.EndRequest += ContextEndRequest;
            context.Error += ContextError;
        }

        private void ContextBeginRequest(object sender, EventArgs e) {
            foreach (var sessionFactory in GetSessionFactories()) {
                var localFactory = sessionFactory;

                LazySessionContext.Bind(new Lazy<ISession>(() => BeginSession(localFactory)), sessionFactory);
            }
        }

        private static ISession BeginSession(ISessionFactory sessionFactory) {
            var session = sessionFactory.OpenSession();
            session.BeginTransaction();
            return session;
        }

        private void ContextEndRequest(object sender, EventArgs e) {
            foreach (var sessionfactory in GetSessionFactories()) {
                var session = LazySessionContext.UnBind(sessionfactory);
                if (session == null) continue;
                EndSession(session);
            }
        }

        private void ContextError(object sender, EventArgs e) {
            foreach (var sessionfactory in GetSessionFactories()) {
                var session = LazySessionContext.UnBind(sessionfactory);
                if (session == null) continue;
                EndSession(session, false);
            }
        }

        private static void EndSession(ISession session, bool commitTransaction = true) {
            try {
                if (session.Transaction != null && session.Transaction.IsActive) {
                    if (commitTransaction) {
                        try {
                            session.Transaction.Commit();
                        }
                        catch {
                            session.Transaction.Rollback();
                            throw;
                        }
                    }
                    else {
                        session.Transaction.Rollback();
                    }
                }
            }
            finally {
                if (session.IsOpen)
                    session.Close();

                session.Dispose();
            }
        }

        public void Dispose() { }

        /// <summary>
        /// Retrieves all ISessionFactory instances via IoC
        /// </summary>
        private IEnumerable<ISessionFactory> GetSessionFactories() {
            var sessionFactories = DependencyResolver.Current.GetServices<ISessionFactory>();

            if (sessionFactories == null || !sessionFactories.Any())
                throw new TypeLoadException("At least one ISessionFactory has not been registered with IoC");

            return sessionFactories;
        }
    }
}
