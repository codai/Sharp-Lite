using System;
using System.Web.Mvc;
using SharpLite.Domain.DataInterfaces;
using System.Reflection;

namespace SharpLite.Web.Mvc.ModelBinder
{
    /// <summary>
    /// Used to get a data-layer-agnostic handle to a repository; i.e., it doesn't matter if you're
    /// using NHibernate or something else, you just need to have registered IRepositoryWithTypedId<,>
    /// with the IoC container
    /// </summary>
    internal class EntityRetriever
    {
        internal static object GetEntityFor(Type collectionEntityType, object typedId, Type idType) {
            var entityRepository = CreateEntityRepositoryFor(collectionEntityType, idType);

            return entityRepository.GetType().InvokeMember(
                "Get", BindingFlags.InvokeMethod, null, entityRepository, new[] { typedId });
        }

        private static object CreateEntityRepositoryFor(Type entityType, Type idType) {
            Type concreteRepositoryType = typeof(IRepositoryWithTypedId<,>)
                .MakeGenericType(new[] { entityType, idType });

            object repository = DependencyResolver.Current.GetService(concreteRepositoryType);

            if (repository == null)
                throw new TypeLoadException(concreteRepositoryType.ToString() + " has not been registered with IoC");

            return repository;
        }
    }
}
