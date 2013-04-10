namespace SharpLite.EntityFrameworkProvider
{
    using System;

    public abstract class EntityFrameworkQuery
    {
        private readonly System.Data.Entity.DbContext _dbContext;

        protected EntityFrameworkQuery(System.Data.Entity.DbContext dbContext) {
            if (dbContext == null) throw new ArgumentNullException("dbContext may not be null");

            this._dbContext = dbContext;
        }

        protected System.Data.Entity.DbContext DbContext {
            get {
                return this._dbContext;
            }
        }
    }
}