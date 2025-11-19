//run in mongosh


// seed_all.js

// ---------- Ministries ----------
db.ministries.deleteMany({});
db.ministries.insertMany([
    { _id: "min-1", name: "משרד הפנים", code: "MOI" },
    { _id: "min-2", name: "משרד התחבורה", code: "MOT" }
]);

// ---------- ServiceTypes ----------
db.serviceTypes.deleteMany({});
db.serviceTypes.insertMany([
    { _id: "svc-1", ministryId: "min-1", name: "תעודת זהות", durationMinutes: 30 },
    { _id: "svc-2", ministryId: "min-1", name: "דרכון", durationMinutes: 45 },
    { _id: "svc-3", ministryId: "min-1", name: "רישיון נהיגה", durationMinutes: 30 },
    { _id: "svc-4", ministryId: "min-2", name: "רישוי רכב", durationMinutes: 20 }
]);

// ---------- Offices ----------
db.offices.deleteMany({});
db.offices.insertMany([
    {
        _id: "office-1",
        ministryId: "min-1",
        name: "משרד הפנים – תל אביב",
        address: "רחוב דוגמה 10, תל אביב",
        openingHours: [
            { day: 1, start: "08:00", end: "16:00" },
            { day: 2, start: "08:00", end: "16:00" },
            { day: 3, start: "08:00", end: "16:00" },
            { day: 4, start: "08:00", end: "16:00" },
            { day: 5, start: "08:00", end: "12:00" },
            { day: 6, start: "08:00", end: "12:00" }
        ]
    },
    {
        _id: "office-2",
        ministryId: "min-1",
        name: "משרד הפנים – ירושלים",
        address: "רחוב ירושלים 5, ירושלים",
        openingHours: [
            { day: 1, start: "08:30", end: "16:30" },
            { day: 2, start: "08:30", end: "16:30" }
        ]
    },
    {
        _id: "office-3",
        ministryId: "min-2",
        name: "משרד התחבורה – תל אביב",
        address: "רחוב תחבורה 7, תל אביב",
        openingHours: [
            { day: 1, start: "08:00", end: "16:00" },
            { day: 2, start: "08:00", end: "16:00" }
        ]
    }
]);

// ---------- OfficeServices ----------
db.officeServices.deleteMany({});
db.officeServices.insertMany([
    { officeId: "office-1", serviceTypeId: "svc-1" },
    { officeId: "office-1", serviceTypeId: "svc-2" },
    { officeId: "office-2", serviceTypeId: "svc-3" },
    { officeId: "office-3", serviceTypeId: "svc-4" }
]);

// ---------- Users ----------
db.users.deleteMany({});
db.users.insertMany([
    {
        _id: "user-1",
        externalId: "SSO-9876",
        name: "רבקה כהן",
        contact: { phone: "050-1234567", email: "rivka@example.com" },
        preferredLanguage: "he"
    },
    {
        _id: "user-2",
        externalId: "SSO-1234",
        name: "יוסי לוי",
        contact: { phone: "052-7654321", email: "yossi@example.com" },
        preferredLanguage: "he"
    },
    {
        _id: "admin-1",
        externalId: "SSO-ADMIN1",
        name: "מנהל משרד הפנים",
        contact: { phone: "050-1111111", email: "admin@example.com" },
        preferredLanguage: "he"
    }
]);

// ---------- AppointmentStatusTypes ----------
db.appointmentStatusTypes.deleteMany({});
db.appointmentStatusTypes.insertMany([
    { _id: 1, name: "Booked" },
    { _id: 2, name: "Cancelled" },
    { _id: 3, name: "Completed" },
    { _id: 4, name: "NoShow" }
]);

// ---------- AuditEventTypes ----------
db.auditEventTypes.deleteMany({});
db.auditEventTypes.insertMany([
    { _id: 1, name: "Create" },
    { _id: 2, name: "Update" },
    { _id: 3, name: "CancelByUser" },
    { _id: 4, name: "CancelByOffice" },
    { _id: 5, name: "NoShow" },
    { _id: 6, name: "Complete" },
    { _id: 7, name: "Delete" }
]);

// ---------- AppointmentSlots (דוגמה) ----------
db.appointmentSlots.deleteMany({});

function generateSlots(startDateStr, endDateStr, officeId, serviceTypeId, durationMinutes, capacity) {
    const slots = [];
    const startDate = new Date(startDateStr);
    const endDate = new Date(endDateStr);
    const oneDay = 24 * 60 * 60 * 1000;

    for (let d = new Date(startDate); d <= endDate; d = new Date(d.getTime() + oneDay)) {
        for (let h = 8; h < 16; h++) {
            const slotStart = new Date(d);
            slotStart.setHours(h, 0, 0, 0);
            const slotEnd = new Date(slotStart);
            slotEnd.setMinutes(slotEnd.getMinutes() + durationMinutes);
            slots.push({
                _id: `slot-${officeId}-${serviceTypeId}-${slotStart.toISOString()}`,
                officeId,
                serviceTypeId,
                date: slotStart.toISOString().substring(0, 10),
                startUtc: slotStart,
                endUtc: slotEnd,
                capacity,
                reservedCount: 0
            });
        }
    }
    return slots;
}

let allSlots = [];
allSlots = allSlots.concat(generateSlots("2025-11-25", "2025-11-29", "office-1", "svc-1", 30, 5));
allSlots = allSlots.concat(generateSlots("2025-11-25", "2025-11-29", "office-1", "svc-2", 45, 3));
allSlots = allSlots.concat(generateSlots("2025-11-25", "2025-11-29", "office-2", "svc-3", 30, 4));
allSlots = allSlots.concat(generateSlots("2025-11-25", "2025-11-29", "office-3", "svc-4", 20, 5));

db.appointmentSlots.insertMany(allSlots);
// ---------- Appointments (דוגמה) ----------
db.appointments.deleteMany({});
db.appointments.insertOne({
    _id: "appt-1",
    userId: "user-1",
    officeId: "office-1",
    serviceTypeId: "svc-1",
    slotStartUtc: ISODate("2025-11-25T06:00:00Z"),
    slotEndUtc: ISODate("2025-11-25T06:30:00Z"),
    statusId: 1,
    createdAt: new Date(),
    updatedAt: new Date(),
    metadata: { phone: "050-1234567" },
    correlationId: "corr-123"
});

// ---------- AuditLogs (דוגמה) ----------
db.auditLogs.deleteMany({});
db.auditLogs.insertMany([
    {
        _id: "event-1",
        entityId: "appt-1",
        typeId: 1,
        by: "user-1",
        timestamp: new Date(),
        correlationId: "corr-123",
        payload: {}
    }
]);

print("Seed complete");
