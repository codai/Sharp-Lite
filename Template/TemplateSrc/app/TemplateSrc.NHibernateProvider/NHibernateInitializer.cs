using TemplateSrc.Domain;
using NHibernate.Bytecode;
using NHibernate.Cfg;
using NHibernate.Dialect;
using NHibernate.Mapping.ByCode;
using SharpLite.NHibernateProvider;
using SharpLite.NHibernateProvider.ConfigurationCaching;

namespace TemplateSrc.NHibernateProvider
{
    public class NHibernateInitializer
    {
        public static Configuration Initialize() {
            INHibernateConfigurationCache cache = new NHibernateConfigurationFileCache();

            var mappingAssemblies = new[] { 
                typeof(ActionConfirmation<>).Assembly.GetName().Name
            };

            var configuration = cache.LoadConfiguration(CONFIG_CACHE_KEY, null, mappingAssemblies);

            if (configuration == null) {
                configuration = new Configuration();

                configuration
                    .Proxy(p => p.ProxyFactoryFactory<DefaultProxyFactoryFactory>())
                    .DataBaseIntegration(db => {
                        db.ConnectionStringName = "TemplateSrcConnectionString";
                        db.Dialect<MsSql2008Dialect>();
                    })
                    .AddAssembly(typeof(ActionConfirmation<>).Assembly)
                    .CurrentSessionContext<LazySessionContext>();

                var mapper = new ConventionModelMapper();
                mapper.WithConventions(configuration);

                cache.SaveConfiguration(CONFIG_CACHE_KEY, configuration);
            }

            return configuration;
        }

        private const string CONFIG_CACHE_KEY = "TemplateSrc";
    }
}