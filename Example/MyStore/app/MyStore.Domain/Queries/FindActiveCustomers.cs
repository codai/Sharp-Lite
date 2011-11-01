using System.Linq;

namespace MyStore.Domain.Queries
{
    /// <summary>
    /// Query object to return all customers who are considered active, based on a minimum number of orders
    /// 
    /// This is a "find" vs. a "query"; i.e., this filters the source based using supplied criteria
    /// </summary>
    public static class FindActiveCustomersExtension
    {
        public static IQueryable<Customer> FindActiveCustomers(this IQueryable<Customer> customers) {
            return customers.FindActiveCustomers(MINIMUM_ORDERS_TO_BE_CONSIDERED_ACTIVE);
        }

        public static IQueryable<Customer> FindActiveCustomers(this IQueryable<Customer> customers, int minimumOrders) {
            return customers.Where(c =>
                c.Orders.Count >= minimumOrders);
        }

        private const int MINIMUM_ORDERS_TO_BE_CONSIDERED_ACTIVE = 3;
    }
}
