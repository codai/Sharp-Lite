using NHibernate.Mapping.ByCode;

namespace CaTS.NHibernateProvider.Overrides
{
    internal interface IOverride
    {
        void Override(ModelMapper mapper);
    }
}
