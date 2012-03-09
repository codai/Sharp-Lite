﻿using System;
using System.Web.Mvc;
using CaTS.NHibernateProvider;
using NHibernate;
using SharpLite.Domain.DataInterfaces;
using SharpLite.NHibernateProvider;
using StructureMap;

namespace CaTS.Init
{
    public class DependencyResolverInitializer
    {
        public static void Initialize() {
            _container = new Container(x => {
                x.For<ISessionFactory>()
                    .Singleton()
                    .Use(() => NHibernateInitializer.Initialize().BuildSessionFactory());
                x.For<IEntityDuplicateChecker>().Use<EntityDuplicateChecker>();
                x.For(typeof(IRepository<>)).Use(typeof(Repository<>));
                // Even if you don't use this, it's needed by the SharpModelBinder
                x.For(typeof(IRepositoryWithTypedId<,>)).Use(typeof(RepositoryWithTypedId<,>));
            });

            DependencyResolver.SetResolver(new StructureMapDependencyResolver(_container));
        }

        public static void AddDependency(Type pluginType, Type concreteType) {
            _container.Configure(x => x.For(pluginType).Use(concreteType));
        }

        private static Container _container;
    }
}