using System.ComponentModel;

namespace CleanEvoBPM.Domain.Enums
{
    public enum RecordStatus
    {
        [Description("Active")]
        Active = 1,
        [Description("In active")]
        InActive = 0
    }
}
