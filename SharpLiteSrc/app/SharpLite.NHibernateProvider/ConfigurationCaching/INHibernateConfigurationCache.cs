using NHibernate.Cfg;

namespace SharpLite.NHibernateProvider.ConfigurationCaching
{
    /// <summary>
    ///     Interface for providing caching capability for an <see cref = "Configuration" /> object.
    /// </summary>
    public interface INHibernateConfigurationCache
    {
        /// <summary>
        /// Load the <see cref = "Configuration" /> object from a cache.
        /// </summary>
        /// <param name = "configKey">Key value to provide a unique name to the cached <see cref = "Configuration" />.</param>
        /// <param name = "configPath">NHibernate configuration xml file.  This is used to determine if the 
        ///     cached <see cref = "Configuration" /> is out of date or not.</param>
        /// <param name="mappingAssemblies">List of assemblies containing HBM files.</param>
        /// <returns>If an up to date cached object is available, a <see cref = "Configuration" /> object, otherwise null.</returns>
        Configuration LoadConfiguration(string configKey, string configPath, string[] mappingAssemblies);

        /// <summary>
        /// Save the <see cref = "Configuration" /> object to a cache.
        /// </summary>
        /// <param name = "configKey">Key value to provide a unique name to the cached <see cref = "Configuration" />.</param>
        /// <param name = "config">Configuration object to save.</param>
        void SaveConfiguration(string configKey, Configuration config);
    }
}
