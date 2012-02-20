using TemplateSrc.Domain;
using NHibernate.Bytecode;
using NHibernate.Cfg;
using NHibernate.Dialect;
using NHibernate.Mapping.ByCode;
using SharpLite.NHibernateProvider;

namespace TemplateSrc.NHibernateProvider
{
    public class NHibernateInitializer
    {
        public static Configuration Initialize() {

            INHibernateConfigurationCache cache = new NHibernateConfigurationFileCache();

            string[] mappingAssemblies = new string[] { 
                typeof(ActionConfirmation<>).Assembly.GetName().Name
            };
            string configFile = null; //NHibernate.config is not used;
            string configKey = "TemplateSrc";

            Configuration configuration = cache.LoadConfiguration(configKey, configFile, mappingAssemblies);

            if (configuration == null)
            {
                configuration = new Configuration();

                configuration
                    .Proxy(p => p.ProxyFactoryFactory<DefaultProxyFactoryFactory>())
                    .DataBaseIntegration(db =>
                    {
                        db.ConnectionStringName = "TemplateSrcConnectionString";
                        db.Dialect<MsSql2008Dialect>();
                    })
                    .AddAssembly(typeof(ActionConfirmation<>).Assembly)
                    .CurrentSessionContext<LazySessionContext>();

                ConventionModelMapper mapper = new ConventionModelMapper();
                mapper.WithConventions(configuration);

                cache.SaveConfiguration(configKey, configuration);
            }

            return configuration;
        }
    }
}