using System;
using System.Linq;
using System.Reflection;
using NHibernate;
using NHibernate.Criterion;
using SharpLite.Domain;
using SharpLite.Domain.DataInterfaces;

namespace SharpLite.NHibernateProvider
{
    public class EntityDuplicateChecker : IEntityDuplicateChecker
    {
        public EntityDuplicateChecker(ISessionFactory sessionFactory) {
            if (sessionFactory == null) throw new ArgumentNullException("sessionFactory may not be null");

            _sessionFactory = sessionFactory;
        }

        /// <summary>
        ///     Provides a behavior specific repository for checking if a duplicate exists of an existing entity.
        /// </summary>
        public bool DoesDuplicateExistWithTypedIdOf<TId>(IEntityWithTypedId<TId> entity) {
            if (entity == null) throw new ArgumentNullException("Entity may not be null when checking for duplicates");

            ISession session = _sessionFactory.GetCurrentSession();
            FlushMode previousFlushMode = session.FlushMode;

            // We do NOT want this to flush pending changes as checking for a duplicate should 
            // only compare the object against data that's already in the database
            session.FlushMode = FlushMode.Never;

            var criteria =
                session.CreateCriteria(entity.GetType()).Add(Restrictions.Not(Restrictions.Eq("Id", entity.Id))).
                    SetMaxResults(1);

            AppendSignaturePropertyCriteriaTo(criteria, entity);
            bool doesDuplicateExist = criteria.List().Count > 0;
            session.FlushMode = previousFlushMode;
            return doesDuplicateExist;
        }

        private static void AppendEntityCriteriaTo<TId>(
            ICriteria criteria, PropertyInfo signatureProperty, object propertyValue) {
            criteria.Add(
                propertyValue != null
                    ? Restrictions.Eq(signatureProperty.Name + ".Id", ((IEntityWithTypedId<TId>)propertyValue).Id)
                    : Restrictions.IsNull(signatureProperty.Name + ".Id"));
        }

        private static void AppendStringPropertyCriteriaTo(
            ICriteria criteria, PropertyInfo signatureProperty, object propertyValue) {
            criteria.Add(
                propertyValue != null
                    ? Restrictions.InsensitiveLike(signatureProperty.Name, propertyValue.ToString(), MatchMode.Exact)
                    : Restrictions.IsNull(signatureProperty.Name));
        }

        private static void AppendValuePropertyCriteriaTo(
            ICriteria criteria, PropertyInfo signatureProperty, object propertyValue) {
            criteria.Add(
                propertyValue != null
                    ? Restrictions.Eq(signatureProperty.Name, propertyValue)
                    : Restrictions.IsNull(signatureProperty.Name));
        }

        private void AppendDateTimePropertyCriteriaTo(
            ICriteria criteria, PropertyInfo signatureProperty, object propertyValue) {
            criteria.Add(
                (DateTime)propertyValue > this.uninitializedDatetime
                    ? Restrictions.Eq(signatureProperty.Name, propertyValue)
                    : Restrictions.IsNull(signatureProperty.Name));
        }

        private void AppendSignaturePropertyCriteriaTo<TId>(ICriteria criteria, IEntityWithTypedId<TId> entity) {
            foreach (var signatureProperty in entity.GetSignatureProperties()) {
                var propertyType = signatureProperty.PropertyType;
                var propertyValue = signatureProperty.GetValue(entity, null);

                if (propertyType.IsEnum) {
                    criteria.Add(Restrictions.Eq(signatureProperty.Name, (int)propertyValue));
                }
                else if (
                    propertyType.GetInterfaces().Any(
                        x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(IEntityWithTypedId<>))) {
                    AppendEntityCriteriaTo<TId>(criteria, signatureProperty, propertyValue);
                }
                else if (propertyType == typeof(DateTime)) {
                    this.AppendDateTimePropertyCriteriaTo(criteria, signatureProperty, propertyValue);
                }
                else if (propertyType == typeof(string)) {
                    AppendStringPropertyCriteriaTo(criteria, signatureProperty, propertyValue);
                }
                else if (propertyType.IsValueType) {
                    AppendValuePropertyCriteriaTo(criteria, signatureProperty, propertyValue);
                }
                else {
                    throw new ApplicationException(
                        "Can't determine how to use " + entity.GetType() + "." + signatureProperty.Name +
                        " when looking for duplicate entries. To remedy this, " +
                        "you can create a custom validator or report an issue to the S#arp Architecture " +
                        "project, detailing the type that you'd like to be accommodated.");
                }
            }
        }

        private readonly ISessionFactory _sessionFactory;
        private readonly DateTime uninitializedDatetime = default(DateTime);
    }
}
