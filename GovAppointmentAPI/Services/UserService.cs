using GovAppointmentAPI.Contracts;
using GovAppointmentAPI.data;
using GovAppointmentAPI.models;
using MongoDB.Driver;

namespace GovAppointmentAPI.Services
{
    public class UserService:IUserService
    {
       
        private readonly IMongoCollection<User> _users;

        public UserService(MongoDbContext context)
        {
            _users = context.Users;
        }
        /// <summary>
        /// הפונקציה מחזירה את המשתמש אם קיים 
        /// הכנסת משתמש חדש אם לא קיים
        /// </summary>
        /// <param name="externalId"></param>
        /// <param name="name"></param>
        /// <param name="phone"></param>
        /// <param name="email"></param>
        /// <returns></returns>
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
