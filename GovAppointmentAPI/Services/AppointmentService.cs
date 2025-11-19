using GovAppointmentAPI.Contracts;
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

        private readonly int _reserveRetries = 1; // אפשר להגדיל אם רוצים


        public AppointmentService(IMongoDatabase db)
        {
            _appointments = db.GetCollection<Appointment>("appointments");

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
        //    public async Task<Appointment?> BookSlotAsync(string slotId, string userId)
        //    {
        //        // 1. בניית הפילטר לבדיקה atomic של פנוי
        //        //       FilterDefinition<AppointmentSlot> filter = Builders<AppointmentSlot>.Filter.And(
        //        //    Builders<AppointmentSlot>.Filter.Eq(s => s.Id, slotId),
        //        //    Builders<AppointmentSlot>.Filter.Expr(
        //        //        new BsonDocument
        //        //        {
        //        //           { "$lt", new BsonArray { "$reservedCount", "$capacity" } }
        //        //        }
        //        //    )
        //        //);
        //        ///

        //        ///בחר את התור שבו השדה Id שווה ל־slotId
        //        //ובודק אם הערך של reservedCount קטן מהערך של capacity
        //        var filter = new BsonDocument
        //{
        //    { "$and", new BsonArray
        //        {
        //            new BsonDocument("Id", slotId),
        //            new BsonDocument("$expr", new BsonDocument("$lt", new BsonArray { "$reservedCount", "$capacity" }))
        //        }
        //    }
        //};
        //        // 2. עדכון תור בצורה atomic + העלאת reservedCount ב־1
        //        var update = Builders<AppointmentSlot>.Update.Inc(s => s.ReservedCount, 1);

        //        var options = new FindOneAndUpdateOptions<AppointmentSlot>
        //        {
        //            ReturnDocument = ReturnDocument.After
        //        };

        //        var slot = await _slots.FindOneAndUpdateAsync(filter, update, options);

        //        if (slot == null)
        //        {
        //            // תור תפוס או לא קיים
        //            return null;
        //            //או
        //             // לא הצלחנו לתפוס את התור במשרד (מלא או לא נמצא)
        //             //       throw new SlotUnavailableException("Slot is full or not available.");
        //        }

        //        // 3. יצירת Appointment חדש
        //        var appointment = new Appointment
        //        {
        //            Id = Guid.NewGuid().ToString(),
        //            UserId = userId,
        //            OfficeId = slot.OfficeId,
        //            ServiceTypeId = slot.ServiceTypeId,
        //            SlotStartUtc = slot.StartUtc,
        //            SlotEndUtc = slot.EndUtc,
        //            StatusId = 1, // Booked
        //            CreatedAt = DateTime.UtcNow,
        //            UpdatedAt = DateTime.UtcNow,
        //            CorrelationId = Guid.NewGuid().ToString(),
        //            Metadata = new { } // אפשר למלא פרטים נוספים
        //        };

        //        await _appointments.InsertOneAsync(appointment);

        //        return appointment;
        //    }

        ///// <summary>
        ///// מנסה להזמין סלוט אטומית; במידה והצליח - יוצר Appointment ומוסיף Audit.
        ///// זורק SlotUnavailableException אם לא נשאר מקום.
        ///// </summary>
        //public async Task<string> BookSlotAtomicAsync(string slotId, string userId, string correlationId, CancellationToken ct = default)
        //{
        //    if (string.IsNullOrWhiteSpace(slotId)) throw new ArgumentNullException(nameof(slotId));

        //    AppointmentSlot? reservedSlot = null;
        //    int attempt = 0;
        //    while (attempt <= _reserveRetries)
        //    {
        //        attempt++;
        //        try
        //        {
        //            // Filter: מתאים לפי id && reservedCount < capacity && (במידת הצורך) startUtc > now
        //            var filter = Builders<AppointmentSlot>.Filter.And(
        //                Builders<AppointmentSlot>.Filter.Eq(s => s.Id, slotId),
        //                 //Builders<AppointmentSlot>.Filter.Expr(new BsonDocument("$lt", new BsonArray { "$reservedCount", "$capacity" }))
        //                 Builders<AppointmentSlot>.Filter.Where(slot => slot.reservedCount < slot.capacity);

        //            );

        //            // Update: +1 לreservedCount
        //            var update = Builders<AppointmentSlot>.Update.Inc(s => s.ReservedCount, 1);

        //            // החזרת המסמך לאחר העדכון
        //            var options = new FindOneAndUpdateOptions<AppointmentSlot>
        //            {
        //                ReturnDocument = ReturnDocument.After
        //            };

        //            reservedSlot = await _slots.FindOneAndUpdateAsync(filter, update, options, ct);

        //            if (reservedSlot == null)
        //            {
        //                // לא הצלחנו לתפוס את התור במשרד (מלא או לא נמצא)
        //                throw new SlotUnavailableException("Slot is full or not available.");
        //            }

        //            // הצלחנו לשמור את התור - יוצרים את הפגישה
        //            var appt = new Appointment
        //            {
        //                Id = Guid.NewGuid().ToString("D"),
        //                UserId = userId,
        //                OfficeId = reservedSlot.OfficeId,
        //                ServiceTypeId = reservedSlot.ServiceTypeId,
        //                SlotStartUtc = reservedSlot.StartUtc,
        //                SlotEndUtc = reservedSlot.EndUtc,
        //                StatusId = (int)AppointmentStatusType.Booked,
        //                CreatedAt = DateTime.UtcNow,
        //                UpdatedAt = DateTime.UtcNow,
        //                CorrelationId = correlationId
        //            };

        //            await _appointments.InsertOneAsync(appt, cancellationToken: ct);

        //            // Insert audit log
        //            await InsertAuditAsync(appt.Id, AuditEventType.Create, userId, correlationId, ct);

        //            return appt.Id;
        //        }
        //        catch (SlotUnavailableException)
        //        {
        //            // לא להיתקל בretry אם אין מקום - נזרוק החוצה
        //            throw;
        //        }
        //        catch (MongoCommandException ex) when (IsTransient(ex) && attempt <= _reserveRetries)
        //        {
        //            // ניתן לבצע retry עבור שגיאות זמניות של הרשת/שרת
        //            // לוגינג אם יש
        //        }
        //        catch (Exception)
        //        {
        //            // במקרה של שגיאה אחרי שהגברנו reservedCount (rare), כדאי לשקול compesating action:
        //            // לדוגמה, להקטין reservedCount בחזרה אם לא הצלחנו ליצר Appointment.
        //            // אך כאן, מכיוון שה-Insertion של appointment קורה מיד לאחר ה-reserve, הסיכוי למצב זה נמוך.
        //            throw;
        //        }
        //    }

        //    throw new SlotUnavailableException("Failed to reserve slot after retries.");
        //}

        //private async Task InsertAuditAsync(string entityId, AuditEventType type, string by, string corr, CancellationToken ct = default)
        //{
        //    var log = new AuditLog
        //    {
        //        Id = Guid.NewGuid().ToString("D"),
        //        EntityId = entityId,
        //        TypeId = (int)type,
        //        By = by,
        //        Timestamp = DateTime.UtcNow,
        //        CorrelationId = corr,
        //        Payload = new { } // אפשר למלא פרטי שינוי
        //    };

        //    await _audit.InsertOneAsync(log, cancellationToken: ct);
        //}

        //private bool IsTransient(MongoCommandException ex)
        //{
        //    // פישוט: בודקים קוד שגיאה או מסר
        //    // אפשר להרחיב לרשימה של קוד שגיאות לזמניות
        //    return false;
        //}

        // ביטול תור - עדכון סטטוס + audit
        //public async Task CancelByUserAsync(string apptId, string userId, string correlationId, CancellationToken ct = default)
        //{
        //    var update = Builders<Appointment>.Update
        //        .Set(a => a.StatusId, (int)AppointmentStatusType.Cancelled)
        //        .Set(a => a.UpdatedAt, DateTime.UtcNow);

        //    var result = await _appointments.UpdateOneAsync(a => a.Id == apptId, update, cancellationToken: ct);

        //    // אופציונלי: להקטין reservedCount ב־slot המקושר אם רוצים לשחרר מקום מיד
        //    var appt = await _appointments.Find(a => a.Id == apptId).FirstOrDefaultAsync(ct);
        //    if (appt != null)
        //    {
        //        var slotFilter = Builders<AppointmentSlot>.Filter.And(
        //            Builders<AppointmentSlot>.Filter.Eq(s => s.OfficeId, appt.OfficeId),
        //            Builders<AppointmentSlot>.Filter.Eq(s => s.ServiceTypeId, appt.ServiceTypeId),
        //            Builders<AppointmentSlot>.Filter.Eq(s => s.StartUtc, appt.SlotStartUtc)
        //        );

        //        // attempt to decrement reservedCount safely (min 0)
        //        var decUpdate = Builders<AppointmentSlot>.Update.Inc(s => s.ReservedCount, -1);
        //        await _slots.UpdateOneAsync(slotFilter, decUpdate, cancellationToken: ct);
        //    }

        //    await InsertAuditAsync(apptId, AuditEventType.CancelByUser, userId, correlationId, ct);
        //}
    }
}

