using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace GovAppointmentAPI.models
{
    public class AppointmentSlot
    {
        public string Id { get; set; } = default!;
        public string OfficeId { get; set; } = default!;
        public string ServiceTypeId { get; set; } = default!;
        public DateTime StartUtc { get; set; }
        public DateTime EndUtc { get; set; }
        public int Capacity { get; set; }
        public int ReservedCount { get; set; }
    }
}
