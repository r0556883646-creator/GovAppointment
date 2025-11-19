using GovAppointmentAPI.models;

namespace GovAppointmentAPI.Contracts
{
    public interface IUserService
    {
        Task<User> GetOrCreateUserAsync(string externalId);
    }
}
