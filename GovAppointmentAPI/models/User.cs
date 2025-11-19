
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
namespace GovAppointmentAPI.models
{


    public class User
    {
        [BsonId]
        public string Id { get; set; } = Guid.NewGuid().ToString();  // מזהה ייחודי פנימי
        [BsonElement("externalId")]
        public string ExternalId { get; set; }  // מזהה חיצוני, לדוגמה SSO
        [BsonElement("name")]
        public string Name { get; set; }
        [BsonElement("contact")]
        public ContactInfo Contact { get; set; }  // פרטי התקשרות
        [BsonElement("preferredLanguage")]
        public string PreferredLanguage { get; set; } = "he"; // ברירת מחדל: עברית
    }

    public class ContactInfo
    {
        [BsonElement("phone")]
        public string Phone { get; set; }
        [BsonElement("email")]
        public string Email { get; set; }
    }
}

