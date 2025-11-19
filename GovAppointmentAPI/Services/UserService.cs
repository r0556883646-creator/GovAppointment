using GovAppointmentAPI.models;
using MongoDB.Driver;

namespace GovAppointmentAPI.Services
{
    public class UserService
    {
        private readonly IMongoCollection<User> _users;

        public UserService(IMongoDatabase db)
        {
            _users = db.GetCollection<User>("users");
        }

        public async Task<User> GetOrCreateUserAsync(string externalId, string name, string phone, string email)
        {
            var user = await _users.Find(u => u.ExternalId == externalId).FirstOrDefaultAsync();
            if (user != null) return user;

            user = new User
            {
                ExternalId = externalId,
                Name = name,
                Contact = new ContactInfo { Phone = phone, Email = email }
            };

            await _users.InsertOneAsync(user);
            return user;
        }
    }
}
