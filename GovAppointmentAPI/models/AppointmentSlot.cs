using GovAppointmentAPI.models;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using static System.Runtime.InteropServices.JavaScript.JSType;
namespace GovAppointmentAPI.models
{
    public class AppointmentSlot
    {
        [BsonElement("_id")]
        public string Id { get; set; } = default!;
        [BsonElement("officeId")]
        public string OfficeId { get; set; } = default!;
        [BsonElement("serviceTypeId")]
        public string ServiceTypeId { get; set; } = default!;
        public string date { get; set; }
        [BsonElement("startUtc")]
       
        public DateTime StartUtc { get; set; }
        [BsonElement("endUtc")]
        public DateTime EndUtc { get; set; }
        [BsonElement("capacity")]
        public int Capacity { get; set; }
        [BsonElement("reservedCount")]
        public int ReservedCount { get; set; }
    }
}

