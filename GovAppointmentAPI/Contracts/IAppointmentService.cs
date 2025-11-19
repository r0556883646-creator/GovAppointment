using GovAppointmentAPI.models;
namespace GovAppointmentAPI.Contracts
{
    public interface IAppointmentService
    {
        Task<Appointment> CreateAppointmentAsync(Appointment appointment);
        Task<Appointment?> GetAppointmentByIdAsync(string id);
        Task<List<Appointment>> GetAppointmentsForDayAndServAsync(string serviceTypeId, string officeId, DateTime date);
    }
}
