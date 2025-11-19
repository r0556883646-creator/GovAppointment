namespace GovAppointmentAPI.Contracts
{
    public interface IAuditService
    {
        Task CreateLogAsync(string entityId, int typeId, string by, string? correlationId = null, object? payload = null);
    }
}
