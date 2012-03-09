namespace CaTS.Domain
{
    public interface IContextRegistry
    {
        StaffMember GetLoggedInStaffMember();
    }
}
