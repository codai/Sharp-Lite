﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using NHibernate.Cfg;

namespace SharpLite.NHibernateProvider.ConfigurationCaching
{
    /// <summary>
    /// File cache implementation of INHibernateConfigurationCache.  Saves and loads a
    /// serialized version of <see cref="Configuration"/> to a temporary file location.
    /// </summary>
    /// <remarks>Serializing a <see cref="Configuration"/> object requires that all components
    /// that make up the Configuration object be Serializable.  This includes any custom NHibernate 
    /// user types implementing <see cref="NHibernateProvider.UserTypes.IUserType"/>.</remarks>
    public class NHibernateConfigurationFileCache : INHibernateConfigurationCache
    {
        /// <summary>
        /// List of files that the cached configuration is dependent on.  If any of these
        /// files are newer than the cache file then the cache file could be out of date.
        /// </summary>
        protected List<string> dependentFilePaths = new List<string>();

        #region Constructors

        /// <summary>
        /// Initializes new instance of the NHibernateConfigurationFileCache
        /// </summary>
        public NHibernateConfigurationFileCache() {
        }

        /// <summary>
        /// Initializes new instance of the NHibernateConfigurationFileCache using the 
        /// given dependentFilePaths parameter.
        /// </summary>
        /// <param name="dependentFilePaths">List of files that the cached configuration
        /// is dependent upon.</param>
        public NHibernateConfigurationFileCache(IEnumerable<string> dependentFilePaths) {
            AppendToDependentFilePaths(dependentFilePaths);
        }

        #endregion

        #region INHibernateConfigurationCache Members

        /// <summary>
        /// Load the <see cref="Configuration"/> object from a cache.
        /// </summary>
        /// <param name="configKey">Key value to provide a unique name to the cached <see cref="Configuration"/>.</param>
        /// <param name="configPath">NHibernate configuration xml file.  This is used to determine if the 
        /// cached <see cref="Configuration"/> is out of date or not.</param>
        /// <param name="mappingAssemblies">String array containing assembly names where domain classes are defined. 
        /// This is used by the cache to determine if the cached configuration is out of date.</param>
        /// <returns>If an up to date cached object is available, a <see cref="Configuration"/> 
        /// object, otherwise null.</returns>
        public Configuration LoadConfiguration(string configKey, string configPath, string[] mappingAssemblies) {
            string cachePath = CachedConfigPath(configKey);
            AppendToDependentFilePaths(mappingAssemblies);

            if (configPath != null) {
                AppendToDependentFilePaths(configPath);
            }

            if (IsCachedConfigCurrent(cachePath)) {
                return FileCache.RetrieveFromCache<Configuration>(cachePath);
            }

            return null;
        }

        /// <summary>
        /// Save the <see cref="Configuration"/> object to cache to a temporary file.
        /// </summary>
        /// <param name="configKey">Key value to provide a unique name to the cached <see cref="Configuration"/>.</param>
        /// <param name="config">Configuration object to save.</param>
        public void SaveConfiguration(string configKey, Configuration config) {
            string cachePath = CachedConfigPath(configKey);
            FileCache.StoreInCache(config, cachePath);
            File.SetLastWriteTime(cachePath, GetMaxDependencyTime());
        }

        #endregion

        #region Protected Methods

        /// <summary>
        /// Tests if an existing cached configuration file is out of date or not.
        /// </summary>
        /// <param name="cachePath">Location of the cached</param>
        /// <returns>False if the cached config file is out of date, otherwise true.</returns>
        /// <exception cref="ArgumentNullException">Thrown if the cachePath or configPath 
        /// parameters are null.</exception>
        protected virtual bool IsCachedConfigCurrent(string cachePath) {
            if (string.IsNullOrEmpty(cachePath))
                throw new ArgumentNullException("cachePath");

            return (File.Exists(cachePath) && (File.GetLastWriteTime(cachePath) >= GetMaxDependencyTime()));
        }

        /// <summary>
        /// Returns the latest file write time from the list of dependent file paths.
        /// </summary>
        /// <returns>Latest file write time, or '1/1/1980' if list is empty.</returns>
        protected virtual DateTime GetMaxDependencyTime() {
            if ((dependentFilePaths == null) || (dependentFilePaths.Count == 0)) {
                return DateTime.Parse("1/1/1980");
            }

            return dependentFilePaths.Max(n => File.GetLastWriteTime(n));
        }

        /// <summary>
        /// Provide a unique temporary file path/name for the cache file.
        /// </summary>
        /// <param name="configKey"></param>
        /// <returns>Full file path.</returns>
        /// <remarks>The hash value is intended to avoid the file from conflicting
        /// with other applications also using this cache feature.</remarks>
        protected virtual string CachedConfigPath(string configKey) {
            var fileName = string.Format("{0}-{1}.bin", configKey, Assembly.GetCallingAssembly().CodeBase.GetHashCode());

            return Path.Combine(Path.GetTempPath(), fileName);
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Append the given file path to the dependentFilePaths list.
        /// </summary>
        /// <param name="paths">File path.</param>
        private void AppendToDependentFilePaths(string path) {
            this.dependentFilePaths.Add(FindFile(path));
        }

        /// <summary>
        /// Append the given list of file paths to the dependentFilePaths list.
        /// </summary>
        /// <param name="paths"><see cref="IEnumerable{string}"/> list of file paths.</param>
        private void AppendToDependentFilePaths(IEnumerable<string> paths) {
            foreach (var path in paths) {
                dependentFilePaths.Add(FindFile(path));
            }
        }

        /// <summary>
        /// Tests if the file or assembly name exists either in the application's bin folder
        /// or elsewhere.
        /// </summary>
        /// <param name="path">Path or file name to test for existance.</param>
        /// <returns>Full path of the file.</returns>
        /// <remarks>If the path parameter does not end with ".dll" it is appended and 
        /// tested if the dll file exists.</remarks>
        /// <exception cref="FileNotFoundException">Thrown if the file is not found.</exception>
        private string FindFile(string path) {
            if (File.Exists(path)) {
                return path;
            }

            var codeBase = Assembly.GetExecutingAssembly().CodeBase;
            var uri = new UriBuilder(codeBase);
            var uriPath = Uri.UnescapeDataString(uri.Path);
            var codeLocation = Path.GetDirectoryName(uriPath);
            var codePath = Path.Combine(codeLocation, path);

            if (File.Exists(codePath)) {
                return codePath;
            }

            var dllPath = (path.IndexOf(".dll") == -1) ? path.Trim() + ".dll" : path.Trim();

            if (File.Exists(dllPath)) {
                return dllPath;
            }

            var codeDllPath = Path.Combine(codeLocation, dllPath);

            if (File.Exists(codeDllPath)) {
                return codeDllPath;
            }

            throw new FileNotFoundException("Unable to find file.", path);
        }

        #endregion
    }
}