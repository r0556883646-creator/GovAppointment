using MongoDB.Bson.Serialization.Attributes;

namespace GovAppointmentAPI.models
{
    public class Ministry
    {
        [BsonId]
        public string Id { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string Code { get; set; } = default!;
    }
}
