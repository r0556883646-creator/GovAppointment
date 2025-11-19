using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace GovAppointmentAPI.models
{
    public class AuditLog
    {
        [BsonId]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        [BsonElement("entityId")]
        public string EntityId { get; set; }
        [BsonElement("typeId")]
        public int TypeId { get; set; }       // סוג הפעולה
        [BsonElement("by")]
        public string By { get; set; }        // מי ביצע את הפעולה
        [BsonElement("timestamp")]
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
        [BsonElement("correlationId")]
        public string? CorrelationId { get; set; }
        [BsonElement("payload")]
        public object? Payload { get; set; }
    }
}