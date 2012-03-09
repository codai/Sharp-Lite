using CaTS.Domain.Support;
using NHibernate.Mapping.ByCode;

namespace CaTS.NHibernateProvider.Overrides
{
    /// <summary>
    /// Overrides the conventions for the SupportTicket object
    /// </summary>
    public class SupportTicketOverride : IOverride
    {
        public void Override(ModelMapper mapper) {
            mapper.Class<SupportTicket>(map => map.Property(x => x.Status,
                status => {
                    status.Type<StatusCustomType>();
                    status.Column("StatusTypeFk");
                }));
        }
    }
}
