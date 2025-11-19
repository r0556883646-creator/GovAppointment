
using GovAppointmentAPI.models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
namespace GovAppointmentAPI.data
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _db;

        public MongoDbContext(IOptions<MongoDbSettings> settings)
        {
          
            var client = new MongoClient(settings.Value.ConnectionString);
            _db = client.GetDatabase(settings.Value.DatabaseName);
        }
        //private readonly IMongoDatabase _db;
        //public MongoDbContext(IConfiguration config)
        //{
        //    var client = new MongoClient(config.GetConnectionString("MongoDb"));
        //    _db = client.GetDatabase("GovAppointments");
        //}

        public IMongoCollection<Appointment> Appointments => _db.GetCollection<Appointment>("appointments");
        public IMongoCollection<AuditLog> AuditLogs => _db.GetCollection<AuditLog>("auditLogs");
        public IMongoCollection<ServiceType> ServiceTypes => _db.GetCollection<ServiceType>("serviceTypes");
        public IMongoCollection<Ministry> Ministries => _db.GetCollection<Ministry>("ministries");
        public IMongoCollection<Office> Offices => _db.GetCollection<Office>("offices");
        public IMongoCollection<OfficeService> OfficeServices => _db.GetCollection<OfficeService>("officeServices");
        public IMongoCollection<User> Users => _db.GetCollection<User>("users");
        public IMongoCollection<AppointmentSlot> AppointmentSlots => _db.GetCollection<AppointmentSlot>("appointmentSlots");
      
    }
}
public class MongoDbSettings
{
    public string ConnectionString { get; set; }
    public string DatabaseName { get; set; }
}