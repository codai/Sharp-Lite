using NHibernate.Mapping.ByCode;

namespace TemplateSrc.NHibernateProvider.Overrides
{
    internal interface IOverride
    {
        void Override(ModelMapper mapper);
    }
}
