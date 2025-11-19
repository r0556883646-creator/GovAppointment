
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
namespace GovAppointmentAPI.models
{


    public class User
    {
        [BsonId]
        public string Id { get; set; } = Guid.NewGuid().ToString();  // מזהה ייחודי פנימי

        public string ExternalId { get; set; }  // מזהה חיצוני, לדוגמה SSO
        public string Name { get; set; }

        public ContactInfo Contact { get; set; }  // פרטי התקשרות

        public string PreferredLanguage { get; set; } = "he"; // ברירת מחדל: עברית
    }

    public class ContactInfo
    {
        public string Phone { get; set; }
        public string Email { get; set; }
    }
}

