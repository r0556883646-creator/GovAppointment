using GovAppointmentAPI.models;

namespace GovAppointmentAPI.Contracts
{
    public interface ISlotService
    {
        Task<AppointmentSlot> GetAvailableSlotAsync(string slotId);
    }
}
