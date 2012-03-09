using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace SharpLite.NHibernateProvider.ConfigurationCaching
{
    public static class FileCache
    {
        /// <summary>
        /// Deserializes a data file into an object of type {T}.
        /// </summary>
        /// <typeparam name = "T">Type of object to deseralize and return.</typeparam>
        /// <param name = "path">Full path to file containing seralized data.</param>
        /// <returns>If object is successfully deseralized, the object of type {T}, 
        /// otherwise null.</returns>
        /// <exception cref = "ArgumentNullException">Thrown if the path parameter is null or empty.</exception>
        public static T RetrieveFromCache<T>(string path) where T : class {
            if (string.IsNullOrEmpty(path)) {
                throw new ArgumentNullException("path");
            }

            try {
                using (var file = File.Open(path, FileMode.Open)) {
                    var bf = new BinaryFormatter();
                    return bf.Deserialize(file) as T;
                }
            }
            catch {
                // Return null if the object can't be deseralized
                return null;
            }
        }

        /// <summary>
        /// Serialize the given object of type {T} to a file at the given path.
        /// </summary>
        /// <typeparam name = "T">Type of object to serialize.</typeparam>
        /// <param name = "obj">Object to serialize and store in a file.</param>
        /// <param name = "path">Full path of file to store the serialized data.</param>
        /// <exception cref = "ArgumentNullException">Thrown if obj or path parameters are null.</exception>
        public static void StoreInCache<T>(T obj, string path) where T : class {
            if (obj == null) {
                throw new ArgumentNullException("obj");
            }

            if (string.IsNullOrEmpty(path)) {
                throw new ArgumentNullException("path");
            }

            using (var file = File.Open(path, FileMode.Create)) {
                new BinaryFormatter().Serialize(file, obj);
            }
        }
    }
}