using System;
using System.Collections.Generic;

namespace MyStore.Domain.ProductMgmt.Queries
{
    /// <summary>
    /// You'll almost NEVER need to explicitly define a query interface.  It's only 
    /// needed if you ever need to leverage the underlying data-access layer (e.g., NHibernate)
    /// directly when the sole use of .NET LINQ within query objects (e.g., 
    /// FindActiveCustomersExtension and QueryForCustomerOrderSummariesExtension) is too limiting.
    /// 
    /// Accordingly, it is far more preferred to use a non NHibernate-specific query object.
    /// </summary>
    public interface IQueryForProductOrderSummaries
    {
        IEnumerable<ProductOrderSummaryDto> GetByDateRange(DateTime startDate, DateTime endDate);
    }
}
