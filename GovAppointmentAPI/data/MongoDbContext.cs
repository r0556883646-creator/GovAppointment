
using GovAppointmentAPI.models;
using MongoDB.Driver;

public class MongoDbContext
{
    private readonly IMongoDatabase _db;
    public MongoDbContext(IConfiguration config)
    {
        var client = new MongoClient(config.GetConnectionString("MongoDb"));
        _db = client.GetDatabase("GovAppointmentsDb");
    }

    public IMongoCollection<Appointment> Appointments => _db.GetCollection<Appointment>("appointments");
    public IMongoCollection<AuditLog> AuditLogs => _db.GetCollection<AuditLog>("auditLogs");
}
