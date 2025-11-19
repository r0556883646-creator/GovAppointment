using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace GovAppointmentAPI.models
{
    public class AuditLog
    {
        [BsonId]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string EntityId { get; set; }
        public int TypeId { get; set; }       // סוג הפעולה
        public string By { get; set; }        // מי ביצע את הפעולה
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
        public string? CorrelationId { get; set; }
        public object? Payload { get; set; }
    }
}