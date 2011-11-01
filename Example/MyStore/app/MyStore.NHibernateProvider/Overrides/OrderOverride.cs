using MyStore.Domain;
using NHibernate.Mapping.ByCode;

namespace MyStore.NHibernateProvider.Overrides
{
    /// <summary>
    /// Overrides the class-conventions for the Order object
    /// </summary>
    public class OrderOverride : IOverride
    {
        public void Override(ModelMapper mapper) {
            mapper.Class<Order>(map => map.Property(x => x.OrderStatus, 
                status => {
                    status.Type<OrderStatusCustomType>();
                    status.Column("OrderStatusTypeFk");
                }));
        }
    }
}
