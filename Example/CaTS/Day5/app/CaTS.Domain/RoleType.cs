using System;

namespace CaTS.Domain
{
    /// <remarks>
    /// All powers of 2 so we can OR them to combine role permissions.
    /// </remarks>
    [Flags]
    public enum RoleType
    {
        Administrator = 1,
        Manager = 2,
        SupportStaff = 4
    }
}
