using System.Web.Mvc;
using TemplateSrc.NHibernateProvider;
using NHibernate;
using SharpLite.Domain.DataInterfaces;
using SharpLite.NHibernateProvider;
using StructureMap;

namespace TemplateSrc.Init
{
    public class DependencyResolverInitializer
    {
        public static void Initialize() {
            Container container = new Container(x => {
                x.For<ISessionFactory>()
                    .Singleton()
                    .Use(() => NHibernateInitializer.Initialize().BuildSessionFactory());
                x.For<IEntityDuplicateChecker>().Use<EntityDuplicateChecker>();
                x.For(typeof(IRepository<>)).Use(typeof(Repository<>));
                x.For(typeof(IRepositoryWithTypedId<,>)).Use(typeof(RepositoryWithTypedId<,>));
            });

            DependencyResolver.SetResolver(new StructureMapDependencyResolver(container));
        }
    }
}