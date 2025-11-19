using MongoDB.Bson.Serialization.Attributes;

namespace GovAppointmentAPI.models
{
    public class ServiceType
    {
        [BsonId]
        public string Id { get; set; } = default!;
        public string MinistryId { get; set; } = default!;
        public string Name { get; set; } = default!;
        public int DurationMinutes { get; set; }
    }
}
