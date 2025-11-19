using GovAppointmentAPI.models;
namespace GovAppointmentAPI.Contracts
{
    public interface IAppointmentService
    {
        Task<Appointment> CreateAppointmentAsync(Appointment appointment);
        Task<Appointment?> GetAppointmentByIdAsync(string id);
        Task<List<Appointment>> GetAppointmentsAsync(string serviceTypeId, string officeId, DateTime date);
    }
}
