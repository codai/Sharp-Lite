using SharpLite.Domain;

namespace SharpLite.Domain.DataInterfaces
{
    public interface IEntityDuplicateChecker
    {
        bool DoesDuplicateExistWithTypedIdOf<TId>(IEntityWithTypedId<TId> entity);
    }
}
