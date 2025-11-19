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
       /// <summary>
       /// הפונקתיה מכניסה לוגים לתיעוד פעילות
       /// </summary>
       /// <param name="entityId"></param>
       /// <param name="typeId"></param>
       /// <param name="by"></param>
       /// <param name="correlationId"></param>
       /// <param name="payload"></param>
       /// <returns></returns>
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
