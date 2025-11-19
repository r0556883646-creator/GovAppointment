using MongoDB.Driver;
using GovAppointmentAPI.models;
using GovAppointmentAPI.utiles;


namespace GovAppointmentAPI.Services
{
    public class AuditService
    {
        private readonly IMongoCollection<AuditLog> _auditLogs;
        public AuditService(IMongoDatabase db) => _auditLogs = db.GetCollection<AuditLog>("auditLogs");

        public async Task CreateLogAsync(string entityId, int typeId, string by, string? correlationId = null, object? payload = null)
        {
            var log = new AuditLog
            {
                EntityId = entityId,
                TypeId = typeId,
                By = by,
                Timestamp = DateTime.UtcNow,
                CorrelationId = correlationId,
                Payload = payload
            };
            await _auditLogs.InsertOneAsync(log);
        }
    }
}
