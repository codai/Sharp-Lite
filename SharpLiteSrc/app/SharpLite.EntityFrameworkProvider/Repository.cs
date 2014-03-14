using System;
using System.Linq;
using SharpLite.Domain;
using SharpLite.Domain.DataInterfaces;
using System.Data.Entity;
using StructureMap;
using System.Linq.Expressions;
using System.Data.Linq.Mapping;
using System.Data.Linq;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;

namespace SharpLite.EntityFrameworkProvider
{
    public class Repository<T> : RepositoryWithTypedId<T, int>, IRepository<T> where T : class
    {
        public Repository() : base() { }
    }

    public class RepositoryWithTypedId<T, TId> : IRepositoryWithTypedId<T, TId> where T : class
    {
        public RepositoryWithTypedId() 
        {
            // Check  the link below if error with parameterless constructor turns up.
            // http://nicholasbarger.com/2012/03/11/fun-and-struggles-with-mvc-no-parameterless-constructor-defined/
            _dbContext = ObjectFactory.GetInstance<SharpLite.EntityFrameworkProvider.DbContext>();
            _dbSet = _dbContext.Context.Set<T>();
        }
        public virtual T Get(TId id) 
        {
            return this._dbSet.Find(id);
        }
        public virtual IQueryable<T> GetAll() 
        {
            // We are using AsNoTracking() to ensure that we don't get the error:
            // Attaching an entity of type [Type] failed because another entity of the same type already has the same primary key value...etc
            // Visit http://www.asp.net/mvc/tutorials/getting-started-with-ef-using-mvc/advanced-entity-framework-scenarios-for-an-mvc-web-application
            // This is why SharpModelBinder doesn't work with Entity Framework at this time, because this error occurs when editing an entity.
            return this._dbSet.AsNoTracking().AsQueryable();
        }
        public virtual T SaveOrUpdate(T entity) 
        {
            if (entity == null)
            {
                throw new ArgumentNullException("Requires an entity to save or update", new Exception(string.Format("The type {0} may not be null", entity.GetType().Name)));
            }

            var e = entity as EntityWithTypedId<TId>;

            if (e.IsTransient())
            {
                var dbEntity = _dbContext.Context.Entry(entity);
                if (dbEntity.State != EntityState.Detached)
                {
                    dbEntity.State = EntityState.Added;
                }
                else
                { 
                    _dbSet.Add(entity);
                }
            }
            else 
            {
                _dbContext.Context.Entry(entity).State = EntityState.Modified;
            }

            return entity;
        }

        /// <summary>
        /// This deletes the object and commits the deletion immediately.  We don't want to delay deletion
        /// until a transaction commits, as it may throw a foreign key constraint exception which we could
        /// likely handle and inform the user about.  Accordingly, this tries to delete right away; if there
        /// is a foreign key constraint preventing the deletion, an exception will be thrown.
        /// </summary>
        public virtual void Delete(T entity) 
        {
            _dbSet.Remove(entity);
            _dbContext.Context.SaveChanges();
        }

        /// <summary>
        /// Future extension to this interface since there's no built in way to determind if an entity exists 
        /// in the database or not. I haven't tried it, though.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool Exists(T entity)
        {
            bool exists = false;

            var ctx = ((IObjectContextAdapter)_dbContext.Context).ObjectContext;
            var objSet = ctx.CreateObjectSet<T>();

            if (_dbContext.Context.Entry(entity).State != EntityState.Detached)
            {
                var entry = _dbContext.Context.Entry(entity);
                var keyprop = objSet.EntitySet.ElementType.KeyMembers.First();
                //if the key is integer we can check based on if id > 0 or not
                if (keyprop.TypeUsage.EdmType.Name == "Int32")
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

        private SharpLite.EntityFrameworkProvider.DbContext _dbContext = null;
        private IDbSet<T> _dbSet = null;
    }
}
