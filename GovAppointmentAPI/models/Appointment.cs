using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace GovAppointmentAPI.models
{
    public class Appointment
    {
        [BsonId]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [BsonElement("userId")]
        public string UserId { get; set; } = default!;
        [BsonElement("officeId")]
        public string OfficeId { get; set; } = default!;
        [BsonElement("serviceTypeId")]
        public string ServiceTypeId { get; set; } = default!;
        [BsonElement("slotStartUtc")]
        public DateTime SlotStartUtc { get; set; }
        [BsonElement("slotEndUtc")]
        public DateTime SlotEndUtc { get; set; }
        [BsonElement("statusId")]
        public int StatusId { get; set; }   // מזהה ל-AppointmentStatusTypes
        [BsonElement("createdAt")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        [BsonElement("updatedAt")]
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        [BsonElement("correlationId")]
        public string? CorrelationId { get; set; } = Guid.NewGuid().ToString();
        [BsonElement("metadata")]
        public object? Metadata { get; set; }

    }
}
