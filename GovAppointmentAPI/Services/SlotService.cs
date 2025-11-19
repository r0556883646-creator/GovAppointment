using GovAppointmentAPI.Contracts;
using GovAppointmentAPI.models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace GovAppointmentAPI.Services
{
    public class SlotService: ISlotService
    {
        private readonly IMongoCollection<AppointmentSlot> _slotCollection;


        public SlotService(IMongoDatabase database)
        {
            _slotCollection = database.GetCollection<AppointmentSlot>("appointmentSlots");
           
        }

        /// <summary>
        /// מחזיר סלוט פנוי ומבצע עדכון אטומי של reservedCount
        /// </summary>
        //        public async Task<AppointmentSlot?> GetAvailableSlotAsync(string slotId)
        //        {
        //            ///בחר את התור שבו השדה Id שווה ל־slotId
        //            //ובודק אם הערך של reservedCount קטן מהערך של capacity
        //            var filter = new BsonDocument
        //{
        //    { "$and", new BsonArray
        //        {
        //            new BsonDocument("Id", slotId),
        //            new BsonDocument("$expr", new BsonDocument("$lt", new BsonArray { "$reservedCount", "$capacity" }))
        //        }
        //    }
        //};
        //            //var filter = Builders<AppointmentSlot>.Filter.And(
        //            //    Builders<AppointmentSlot>.Filter.Eq(s => s.Id, slotId),
        //            //    Builders<AppointmentSlot>.Filter.Lt(s => s.ReservedCount, s => s.Capacity)
        //            //);

        //            var update = Builders<AppointmentSlot>.Update.Inc(s => s.ReservedCount, 1);

        //            // פעולה אטומית: למצוא ולעדכן
        //            var options = new FindOneAndUpdateOptions<AppointmentSlot>
        //            {
        //                ReturnDocument = ReturnDocument.After
        //            };

        //            var slot = await _slotCollection.FindOneAndUpdateAsync(filter, update, options);

        //            return slot; // null אם לא נמצא סלוט פנוי
        //        }
   
        public async Task<AppointmentSlot?> GetAppointmentByIdAsync(string id)
        {
            return await _slotCollection.Find(a => a.Id == id).FirstOrDefaultAsync();
        }
        /// <summary>
        /// מחזיר סלוט פנוי ומבצע עדכון אטומי של reservedCount
        /// </summary>
        /// <param name="slotId"></param>
        /// <returns></returns>
        public async Task<AppointmentSlot> GetAvailableSlotAsync(string slotId)
        {
            //var filter1 = Builders<AppointmentSlot>.Filter.And(
            //    Builders<AppointmentSlot>.Filter.Eq(s => s.Id, slotId),
            //    Builders<AppointmentSlot>.Filter.Lt(s => s.ReservedCount, s => s.Capacity)
            //);
            // בחר את התור שבו השדה Id שווה ל־slotId
            //ובודק אם הערך של reservedCount קטן מהערך של capacity
            var filter = new BsonDocument
            {
                { "$and", new BsonArray
                    {
                        new BsonDocument("Id", slotId),
                        new BsonDocument("$expr", new BsonDocument("$lt", new BsonArray { "$reservedCount", "$capacity" }))
                    }
                }
            };
            var update = Builders<AppointmentSlot>.Update.Inc(s => s.ReservedCount, 1);
            // פעולה אטומית: למצוא ולעדכן
            var options = new FindOneAndUpdateOptions<AppointmentSlot>
            {
                ReturnDocument = ReturnDocument.After
            };

            var slot = await _slotCollection.FindOneAndUpdateAsync(filter, update, options);

            return slot; // null אם לא נמצא סלוט פנוי

        }
    }

    
}
