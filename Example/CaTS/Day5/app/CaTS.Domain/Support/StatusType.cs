using System.ComponentModel;

namespace CaTS.Domain.Support
{
    public enum StatusType
    {
        New = 1,
        [Description("In Progress")]
        InProgress = 2,
        Resolved = 3
    }
}
