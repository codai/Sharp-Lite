using System.Linq;

namespace MyStore.Domain.Queries
{
    /// <summary>
    /// Example of taking a list of users and generating reporting data.
    /// 
    /// This is a "query" vs. a "find"; i.e., this returns a report summary from the source.
    /// </summary>
    public static class QueryForCustomerOrderSummariesExtension
    {
        public static IQueryable<CustomerOrderSummaryDto> QueryForCustomerOrderSummaries(this IQueryable<Customer> customers) {
            return from customer in customers
                   select new CustomerOrderSummaryDto() {
                       FirstName = customer.FirstName,
                       LastName = customer.LastName,
                       OrderCount = customer.Orders.Count
                   };
        }
    }
}
