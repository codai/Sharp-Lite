using System;
using SharpLite.Domain;
using SharpLite.Domain.DataInterfaces;

namespace SharpLite.EntityFrameworkProvider
{
    public class EntityDuplicateChecker : IEntityDuplicateChecker
    {
        /// <summary>
        ///     Provides a behavior specific repository for checking if a duplicate exists of an existing entity.
        /// </summary>
        public bool DoesDuplicateExistWithTypedIdOf<TId>(IEntityWithTypedId<TId> entity) {
            if (entity == null) throw new ArgumentNullException("Entity may not be null when checking for duplicates");

            throw new NotImplementedException();
        }
    }
}
