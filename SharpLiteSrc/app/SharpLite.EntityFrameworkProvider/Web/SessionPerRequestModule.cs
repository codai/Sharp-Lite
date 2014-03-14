using System;
using System.Web;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Linq;
using SharpLite.EntityFrameworkProvider;
using System.Configuration;
using System.Data.SqlClient;
using StructureMap;

namespace SharpLite.EntityFrameworkProvider.Web
{
    public class SessionPerRequestModule : IHttpModule
    {
        public void Init(HttpApplication context) 
        {
            context.BeginRequest += ContextBeginRequest;
            context.EndRequest += ContextEndRequest;
            context.Error += ContextError;
        }

        private void ContextBeginRequest(object sender, EventArgs e) 
        {
            var dbContext = ObjectFactory.GetInstance<DbContext>();
            if (dbContext != null)
            {
                dbContext.BeginTransaction();
            }
        }
        private void ContextEndRequest(object sender, EventArgs e) 
        {
            var dbContext = ObjectFactory.GetInstance<DbContext>();

            if (dbContext != null)
            {
                EndSession(dbContext, true);
            }
        }
        private void ContextError(object sender, EventArgs e)
        {
            var dbContext = ObjectFactory.GetInstance<DbContext>();
            
            if (dbContext != null)
            { 
                EndSession(dbContext, false);
            }
        }
        private static void EndSession(DbContext dbContext, bool commitTransaction = true) 
        {
            try 
            {
                if (dbContext.Transaction != null && dbContext.Context.Database.Connection.State != System.Data.ConnectionState.Closed) 
                {
                    if (commitTransaction) 
                    {
                        try 
                        {
                            dbContext.CommitChanges();
                            dbContext.CommitTransaction();
                        }
                        catch 
                        {
                            dbContext.RollbackTransaction();
                            throw;
                        }
                    }
                    else 
                    {
                        dbContext.RollbackTransaction();
                    }
                }
            }
            finally 
            {
                // Bug? Do we need to dispose the transaction?
                dbContext.Close();
            }
        }

        public void Dispose() { }
    }
}
