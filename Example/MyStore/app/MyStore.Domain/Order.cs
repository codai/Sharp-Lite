using System;
using System.Collections.Generic;
using System.Linq;
using SharpLite.Domain;

namespace MyStore.Domain
{
    public class Order : Entity
    {
        public Order() {
            OrderLineItems = new List<OrderLineItem>();
            OrderStatus = OrderStatusType.Placed;
        }

        public virtual DateTime PlacedOn { get; set; }

        /// <summary>
        /// Maps to an enum type and is not an entity relationship
        /// </summary>
        public virtual OrderStatusType OrderStatus { get; set; }

        /// <summary>
        /// many-to-one from Order to Customer
        /// </summary>
        public virtual Customer Customer { get; set; }

        /// <summary>
        /// one-to-many from Order to OrderLineItem
        /// </summary>
        public virtual IList<OrderLineItem> OrderLineItems { get; protected set; }

        /// <summary>
        /// Example of adding domain business logic to entity
        /// </summary>
        public virtual Money GetTotal() {
            decimal totalAmount = 
                OrderLineItems.Sum<OrderLineItem>(m => m.GetTotal().Amount);

            return new Money(totalAmount);
        }
    }
}
