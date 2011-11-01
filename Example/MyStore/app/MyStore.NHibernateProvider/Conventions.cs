﻿using System;
using System.Collections.Generic;
using System.Linq;
using MyStore.Domain;
using MyStore.NHibernateProvider.Overrides;
using NHibernate.Cfg;
using NHibernate.Cfg.MappingSchema;
using NHibernate.Mapping.ByCode;
using SharpLite.Domain;

namespace MyStore.NHibernateProvider
{
    /// <summary>
    /// Applies global common conventions to the mapped entities. 
    /// For clarity configurations set here can be overriden in 
    /// an entity's specific mapping file.  For example; The Id 
    /// convention here is set to Id but if the Id column 
    /// was mapped in the entity's mapping file then the entity's 
    /// mapping file configuration will take precedence.
    /// </summary>
    internal static class Conventions
    {
        public static void WithConventions(this ConventionModelMapper mapper, Configuration configuration) {
            Type baseEntityType = typeof(Entity);

            mapper.IsEntity((type, declared) => IsEntity(type));
            mapper.IsRootEntity((type, declared) => baseEntityType.Equals(type.BaseType));

            mapper.BeforeMapClass += (modelInspector, type, classCustomizer) => {
                classCustomizer.Id(c => c.Column("Id"));
                classCustomizer.Id(c => c.Generator(Generators.Identity));
                classCustomizer.Table(Inflector.Net.Inflector.Pluralize(type.Name.ToString()));
            };

            mapper.BeforeMapManyToOne += (modelInspector, propertyPath, map) => {
                map.Column(propertyPath.LocalMember.GetPropertyOrFieldType().Name + "Fk");
                map.Cascade(Cascade.Persist);
            };

            mapper.BeforeMapBag += (modelInspector, propertyPath, map) => {
                map.Key(keyMapper => keyMapper.Column(propertyPath.GetContainerEntity(modelInspector).Name + "Fk"));
                map.Cascade(Cascade.All);
            };

            AddConventionOverrides(mapper);

            HbmMapping mapping = mapper.CompileMappingFor(typeof(Customer).Assembly.GetExportedTypes().Where(t => IsEntity(t)));
            configuration.AddDeserializedMapping(mapping, "MyStoreMappings");
        }

        /// <summary>
        /// Determine if type implements IEntityWithTypedId<>
        /// </summary>
        public static bool IsEntity(Type type) {
            return typeof(Entity).IsAssignableFrom(type) && typeof(Entity) != type && !type.IsInterface;
        }

        /// <summary>
        /// Looks through this assembly for any IOverride classes.  If found, it creates an instance
        /// of each and invokes the Override(mapper) method, accordingly.
        /// </summary>
        private static void AddConventionOverrides(ConventionModelMapper mapper) {
            Type overrideType = typeof(IOverride);
            List<Type> types = typeof(IOverride).Assembly.GetTypes()
                .Where(t => overrideType.IsAssignableFrom(t) && t != typeof(IOverride))
                .ToList();

            types.ForEach(t => {
                IOverride conventionOverride = Activator.CreateInstance(t) as IOverride;
                conventionOverride.Override(mapper);
            });
        }
    }
}
