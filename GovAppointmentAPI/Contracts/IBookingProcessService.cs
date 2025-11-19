using GovAppointmentAPI.models;

namespace GovAppointmentAPI.Contracts
{
    public interface IBookingProcessService
    {
        Task<Appointment> ExecuteBookingProcessAsync(string externalUserId, string name, string phone, string email, string slotId);
    }
}
