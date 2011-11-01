using System;
using System.Collections.Generic;
using MyStore.Domain.ProductMgmt.Queries;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Transform;

namespace MyStore.NHibernateProvider.Queries
{
    /// <summary>
    /// Acts as the concrete query object for implementing IQueryForProductOrderSummaries.
    /// You'll ONLY ever need to do this for very exceptive cases wherein you need to 
    /// leverage capabilities of the underlying data-access mechanism.
    /// </summary>
    public class QueryForProductOrderSummaries : IQueryForProductOrderSummaries
    {
        public QueryForProductOrderSummaries(ISessionFactory sessionFactory) {
            _sessionFactory = sessionFactory;
        }

        /// <summary>
        /// This would be much simpler if done via a .NET LINQ query object instead of using
        /// ICriteria.  But I wanted to include an example of using a query object which 
        /// depended directly on the underlying data-access mechanism; e.g., NHibernate
        /// </summary>
        public IEnumerable<ProductOrderSummaryDto> GetByDateRange(DateTime startDate, DateTime endDate) {
            if (startDate > endDate) throw new ArgumentException("startDate must be <= endDate");

            ISession session = _sessionFactory.GetCurrentSession();

            ICriteria criteria = session.CreateCriteria<Domain.Order>()
                .CreateAlias("OrderLineItems", "orderLineItem")
                .CreateAlias("orderLineItem.Product", "product")
                .Add(Expression.Between("PlacedOn", startDate, endDate))
                .SetProjection(Projections.ProjectionList()
                    .Add(Projections.RowCount(), "OrderCount")
                    .Add(Projections.Sum("orderLineItem.Quantity"), "TotalQuantitySold")
                    .Add(Projections.GroupProperty("product.Name"), "ProductName"));

            criteria.SetResultTransformer(Transformers.AliasToBean<ProductOrderSummaryDto>());

            return criteria.List<ProductOrderSummaryDto>();
        }

        private readonly ISessionFactory _sessionFactory;
    }
}
