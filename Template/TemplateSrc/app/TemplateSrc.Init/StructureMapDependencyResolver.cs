using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using StructureMap;

namespace TemplateSrc.Init
{
    /// <summary>
    /// Taken from http://stevesmithblog.com/blog/how-do-i-use-structuremap-with-asp-net-mvc-3/
    /// </summary>
    public class StructureMapDependencyResolver : IDependencyResolver
    {
        public StructureMapDependencyResolver(IContainer container) {
            _container = container;
        }

        public object GetService(Type serviceType) {
            if (serviceType.IsAbstract || serviceType.IsInterface) {
                return _container.TryGetInstance(serviceType);
            }
            else {
                return _container.GetInstance(serviceType);
            }
        }

        public IEnumerable<object> GetServices(Type serviceType) {
            return _container.GetAllInstances(serviceType).Cast<object>();
        }

        private readonly IContainer _container;
    }
}