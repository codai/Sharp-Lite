using MyStore.Domain.ProductMgmt;
using SharpLite.Domain;

namespace MyStore.Domain
{
    public class OrderLineItem : Entity
    {
        /// <summary>
        /// many-to-one from OrderLineItem to Order
        /// </summary>
        public virtual Order Order { get; set; }

        /// <summary>
        /// Money is a component, not a separate entity; i.e., the OrderLineItems table will have 
        /// column for the amount
        /// </summary>
        public virtual Money Price { get; set; }

        /// <summary>
        /// many-to-one from OrderLineItem to Product
        /// </summary>
        public virtual Product Product { get; set; }
        
        public virtual int Quantity { get; set; }

        /// <summary>
        /// Example of adding domain business logic to entity
        /// </summary>
        public virtual Money GetTotal() {
            return new Money(Price.Amount * Quantity);
        }
    }
}
