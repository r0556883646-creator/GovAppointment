namespace GovAppointmentAPI.utiles
{
    public class Const
    {
    }
    // AppointmentStatusType.cs
    public enum AppointmentStatusType
    {
        Booked = 1,
        Cancelled = 2,
        Completed = 3,
        NoShow = 4
    }

    // AuditEventType.cs
    public enum AuditEventType
    {
        Create = 1,
        Update = 2,
        CancelByUser = 3,
        CancelByOffice = 4,
        NoShow = 5,
        Complete = 6,
        Delete = 7
    }
}
