using MyStore.Domain;
using NHibernate.Bytecode;
using NHibernate.Cfg;
using NHibernate.Dialect;
using NHibernate.Mapping.ByCode;
using SharpLite.NHibernateProvider;

namespace MyStore.NHibernateProvider
{
    public class NHibernateInitializer
    {
        public static Configuration Initialize() {
            Configuration configuration = new Configuration();

            configuration
                .Proxy(p => p.ProxyFactoryFactory<DefaultProxyFactoryFactory>())
                .DataBaseIntegration(db => {
                    db.ConnectionStringName = "MyStoreConnectionString";
                    db.Dialect<MsSql2008Dialect>();
                })
                .AddAssembly(typeof(Customer).Assembly)
                .CurrentSessionContext<LazySessionContext>();

            ConventionModelMapper mapper = new ConventionModelMapper();
            mapper.WithConventions(configuration);

            return configuration;
        }
    }
}