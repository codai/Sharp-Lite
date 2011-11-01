using System;
using System.Collections.Generic;
using System.Web;
using NHibernate;
using NHibernate.Context;
using NHibernate.Engine;
using System.Runtime.Remoting.Messaging;

namespace SharpLite.NHibernateProvider
{
    /// <summary>
    /// Taken from http://nhforge.org/blogs/nhibernate/archive/2011/03/03/effective-nhibernate-session-management-for-web-apps.aspx
    /// </summary>
    public class LazySessionContext : ICurrentSessionContext
    {
        public LazySessionContext(ISessionFactoryImplementor factory) {
            _factory = factory;
        }

        /// <summary>
        /// Retrieve the current session for the session factory.
        /// </summary>
        /// <returns></returns>
        public ISession CurrentSession() {
            Lazy<ISession> initializer;
            var currentSessionFactoryMap = GetCurrentFactoryMap();
            
            if (currentSessionFactoryMap == null ||
                !currentSessionFactoryMap.TryGetValue(_factory, out initializer)) {
                return null;
            }

            return initializer.Value;
        }

        /// <summary>
        /// Bind a new sessionInitializer to the context of the sessionFactory.
        /// </summary>
        /// <param name="sessionInitializer"></param>
        /// <param name="sessionFactory"></param>
        public static void Bind(Lazy<ISession> sessionInitializer, ISessionFactory sessionFactory) {
            var map = GetCurrentFactoryMap();
            map[sessionFactory] = sessionInitializer;
        }

        /// <summary>
        /// Unbind the current session of the session factory.
        /// </summary>
        /// <param name="sessionFactory"></param>
        /// <returns></returns>
        public static ISession UnBind(ISessionFactory sessionFactory) {
            var map = GetCurrentFactoryMap();
            var sessionInitializer = map[sessionFactory];
            map[sessionFactory] = null;
            if (sessionInitializer == null || !sessionInitializer.IsValueCreated) return null;
            return sessionInitializer.Value;
        }

        /// <summary>
        /// Provides the CurrentMap of SessionFactories.
        /// If there is no map create/store and return a new one.
        /// </summary>
        /// <returns></returns>
        private static IDictionary<ISessionFactory, Lazy<ISession>> GetCurrentFactoryMap() {
            var currentFactoryMap = FactoryMapInContext;

            if (currentFactoryMap == null) {
                currentFactoryMap = new Dictionary<ISessionFactory, Lazy<ISession>>();
                FactoryMapInContext = currentFactoryMap;
            }

            return currentFactoryMap;
        }

        private static IDictionary<ISessionFactory, Lazy<ISession>> FactoryMapInContext {
            get {
                if (IsInWebContext()) {
                    return HttpContext.Current.Items[CURRENT_SESSION_CONTEXT_KEY] as IDictionary<ISessionFactory, Lazy<ISession>>;
                }
                else {
                    return CallContext.GetData(CURRENT_SESSION_CONTEXT_KEY) as IDictionary<ISessionFactory, Lazy<ISession>>;
                }
            }
            set {
                if (IsInWebContext()) {
                    HttpContext.Current.Items[CURRENT_SESSION_CONTEXT_KEY] = value;
                }
                else {
                    CallContext.SetData(CURRENT_SESSION_CONTEXT_KEY, value);
                }
            }
        }

        private static bool IsInWebContext() {
            return HttpContext.Current != null;
        }

        private readonly ISessionFactoryImplementor _factory;
        private const string CURRENT_SESSION_CONTEXT_KEY = "NHibernateCurrentSessionFactory";
    }
}
