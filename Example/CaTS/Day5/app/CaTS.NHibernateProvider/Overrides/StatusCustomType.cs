using CaTS.Domain.Support;
using NHibernate.Type;

namespace CaTS.NHibernateProvider.Overrides
{
    /// <summary>
    /// Used to inform NHibernate to which enum type the stored int should be mapped
    /// </summary>
    public class StatusCustomType : PersistentEnumType
    {
        public StatusCustomType()
            : base(typeof(StatusType)) { }
    }
}
