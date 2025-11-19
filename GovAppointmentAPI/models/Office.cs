using MongoDB.Bson.Serialization.Attributes;

namespace GovAppointmentAPI.models
{
    public class Office
    {
        [BsonId]
        public string Id { get; set; } = default!;
        public string MinistryId { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string Address { get; set; } = default!;
        public List<OpeningHour> OpeningHours { get; set; } = new List<OpeningHour>();
    }

    public class OpeningHour
    {
        public int Day { get; set; }           // 1=Monday ... 7=Sunday
        public string Start { get; set; } = default!;  // "08:00"
        public string End { get; set; } = default!;    // "16:00"
    }
}
