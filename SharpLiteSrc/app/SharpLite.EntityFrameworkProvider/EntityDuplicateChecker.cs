using System;
using SharpLite.Domain;
using SharpLite.Domain.DataInterfaces;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Core.Metadata.Edm;

namespace SharpLite.EntityFrameworkProvider
{
    public class EntityDuplicateChecker : IEntityDuplicateChecker
    {
        private readonly DbContext _context;

        public EntityDuplicateChecker(DbContext context)
        {
            if (context == null) throw new ArgumentNullException("context may not be null");

            _context = context;
        }

        /// <summary>
        ///     Provides a behavior specific repository for checking if a duplicate exists of an existing entity.
        /// </summary>
        public bool DoesDuplicateExistWithTypedIdOf<TId>(IEntityWithTypedId<TId> entity) {
            if (entity == null) throw new ArgumentNullException("Entity may not be null when checking for duplicates");

            bool exists = false;

            var ctx = ((IObjectContextAdapter)_context).ObjectContext;
            var objSet = ctx.CreateObjectSet<IEntityWithTypedId<TId>>();

            if (_context.Context.Entry(entity).State != System.Data.Entity.EntityState.Detached)
            {
                EdmMember keyprop;
                var entry = _context.Context.Entry(entity);
                bool found = objSet.EntitySet.ElementType.KeyMembers.TryGetValue("Int32", false, out keyprop);
                
                //if the key is integer we can check based on if id > 0 or not
                if (found)
                {
                    int keyval = entry.CurrentValues.GetValue<int>(keyprop.Name);
                    if (keyval > 0)
                    {
                        exists = true;
                    }
                }
                else
                {
                    var databasevalues = entry.GetDatabaseValues();
                    if (databasevalues != null)
                    {
                        exists = true;
                    }
                }
            }

            return exists;
        }
    }
}
