using GovAppointmentAPI.Contracts;
using GovAppointmentAPI.data;

using GovAppointmentAPI.Exceptions;
using GovAppointmentAPI.models;
using GovAppointmentAPI.utiles;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace GovAppointmentAPI.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IMongoCollection<Appointment> _appointments;

        public AppointmentService(MongoDbContext context)
        {
            _appointments = context.Appointments;
        }



        public async Task<Appointment> CreateAppointmentAsync(Appointment appointment)
        {
            // הכנסה למסד
            appointment.Id ??= Guid.NewGuid().ToString();
            appointment.CreatedAt = DateTime.UtcNow;
            appointment.UpdatedAt = DateTime.UtcNow;

            await _appointments.InsertOneAsync(appointment);

            return appointment;
        }
       
        public async Task<Appointment?> GetAppointmentByIdAsync(string id)
        {
            return await _appointments.Find(a => a.Id == id).FirstOrDefaultAsync();
        }
        /// <summary>
        /// הפונקציה מחזירה את כל הפגישות הקימות בטבלה בכל הסטטוסים  
        /// עבור הנתונים: משרד סוג שירות ותתאריך מסוים
        /// </summary>
        /// <param name="serviceTypeId"></param>
        /// <param name="officeId"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public async Task<List<Appointment>> GetAppointmentsAsync(string serviceTypeId, string officeId, DateTime date)
        {
            // יוצר טווח של התחלת יום והתחלת היום הבא
            var startOfDay = date.Date;
            var endOfDay = startOfDay.AddDays(1);

            var filter = Builders<Appointment>.Filter.Eq(a => a.ServiceTypeId, serviceTypeId) &
                         Builders<Appointment>.Filter.Eq(a => a.OfficeId, officeId) &
            Builders<Appointment>.Filter.Gte(a => a.SlotStartUtc, startOfDay) &
            Builders<Appointment>.Filter.Lt(a => a.SlotStartUtc, endOfDay);

            return await _appointments.Find(filter).ToListAsync();
        }
    }
}

