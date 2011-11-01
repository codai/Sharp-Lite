using NHibernate.Mapping.ByCode;

namespace MyStore.NHibernateProvider.Overrides
{
    internal interface IOverride
    {
        void Override(ModelMapper mapper);
    }
}
