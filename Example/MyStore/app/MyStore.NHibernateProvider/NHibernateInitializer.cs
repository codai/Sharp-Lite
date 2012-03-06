using System.Reflection;
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
        public static Configuration Initialize()
        {
            INHibernateConfigurationCache cache = new NHibernateConfigurationFileCache();

            string[] mappingAssemblies = new string[] { 
                typeof(Customer).Assembly.GetName().Name
            };
            string configFile = null; //NHibernate.config is not used;
            string configKey = "MyStore";

            Configuration configuration = cache.LoadConfiguration(configKey, configFile, mappingAssemblies);

            if (configuration == null)
            {
                configuration = new Configuration();

                configuration
                    .Proxy(p => p.ProxyFactoryFactory<DefaultProxyFactoryFactory>())
                    .DataBaseIntegration(db =>
                    {
                        db.ConnectionStringName = "MyStoreConnectionString";
                        db.Dialect<MsSql2008Dialect>();
                    })
                    .AddAssembly(typeof(Customer).Assembly)
                    .CurrentSessionContext<LazySessionContext>();

                ConventionModelMapper mapper = new ConventionModelMapper();
                mapper.WithConventions(configuration);

                cache.SaveConfiguration(configKey, configuration);
            }

            return configuration;
        }
    }
}