using GovAppointmentAPI.Contracts;
using GovAppointmentAPI.models;
using GovAppointmentAPI.utiles;

namespace GovAppointmentAPI.Services
{
    public class BookingProcessService: IBookingProcessService
    {
        private readonly IUserService _userService;
        private readonly ISlotService _slotService;
        private readonly IAppointmentService _appointmentService;
        private readonly IAuditService _auditService;

        public BookingProcessService(
            IUserService userService,
            ISlotService slotService,
            IAppointmentService appointmentService,
            IAuditService auditService)
        {
            _userService = userService;
            _slotService = slotService;
            _appointmentService = appointmentService;
            _auditService = auditService;
        }
        public async Task<Appointment> ExecuteBookingProcessAsync(string externalUserId,  string name, string phone, string email,string slotId)
        {
            // 1. קבלת או יצירת משתמש
            var userRet = await _userService.GetOrCreateUserAsync(externalUserId,name,phone,email);
            if (userRet == null)
                throw new Exception("User not available");

            // 2. בדיקת זמינות סלוט
            var availableSlotRet= await _slotService.GetAvailableSlotAsync(slotId);
           
            if (availableSlotRet == null)
                throw new Exception("Slot not available");

         

            // 3. בניית האובייקט Appointment עם כל הנתונים
            var appointment = new Appointment
            {
                Id = Guid.NewGuid().ToString(),
                UserId = userRet.Id,
                OfficeId = availableSlotRet.OfficeId,
                ServiceTypeId = availableSlotRet.ServiceTypeId,
                SlotStartUtc = availableSlotRet.StartUtc,
                SlotEndUtc = availableSlotRet.EndUtc,
                StatusId = (int)AppointmentStatusType.Booked,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                CorrelationId = Guid.NewGuid().ToString()
            };

            // 4. שליחה לשירות שיוצר את הפגישה במסד
            var appointmentRet= await _appointmentService.CreateAppointmentAsync(appointment);

            // 5. לוג
            await _auditService.CreateLogAsync(appointmentRet.Id, (int)AuditEventType.Create, userRet.Id, appointment.CorrelationId);


            return appointmentRet;
        }
    }
}
