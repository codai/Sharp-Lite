using System;
using MyStore.Domain;
using NHibernate.Type;

namespace MyStore.NHibernateProvider.Overrides
{
    /// <summary>
    /// Used to inform NHibernate to which enum type the stored int should be mapped
    /// </summary>
    [Serializable]
    public class OrderStatusCustomType : PersistentEnumType
    {
        public OrderStatusCustomType()
            : base(typeof(OrderStatusType)) { }
    }
}
