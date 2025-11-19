using GovAppointmentAPI.Contracts;
using GovAppointmentAPI.data;
using GovAppointmentAPI.models;
using GovAppointmentAPI.utiles;
using MongoDB.Driver;


namespace GovAppointmentAPI.Services
{
    public class AuditService: IAuditService
    {
        private readonly IMongoCollection<AuditLog> _auditLogs;

        public AuditService(MongoDbContext context)
        {
            _auditLogs = context.AuditLogs;
        }

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
