using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace GovAppointmentAPI.models
{
    public class Appointment
    {
        [BsonId]
        public string Id { get; set; } = Guid.NewGuid().ToString();


        public string UserId { get; set; } = default!;
        public string OfficeId { get; set; } = default!;
        public string ServiceTypeId { get; set; } = default!;

        public DateTime SlotStartUtc { get; set; }
        public DateTime SlotEndUtc { get; set; }

        public int StatusId { get; set; }   // מזהה ל-AppointmentStatusTypes
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public string? CorrelationId { get; set; } = Guid.NewGuid().ToString();
        public object? Metadata { get; set; }

    }
}
