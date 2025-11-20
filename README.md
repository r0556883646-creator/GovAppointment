# GovAppointmentAPI

מערכת **GovAppointmentAPI** היא שירות API לניהול זימון תורים במשרדי הממשלה, עם ממשק מבוסס REST ו‑MongoDB כמאגר הנתונים.

---

## מבנה הנתונים

### Ministries
טבלת משרדים.
- `Id` – מזהה ייחודי
- `Name` – שם המשרד
- `Description` – תיאור אופציונלי

### ServiceTypes
טבלת סוגי שירותים שמציע המשרד.
- `Id` – מזהה ייחודי של סוג שירות
- `Name` – שם השירות
- `Description` – תיאור השירות

### Offices
טבלת סניפים/משרדים של הארגון.
- `Id` – מזהה ייחודי של סניף
- `MinistryId` – מזהה המשרד אליו שייך הסניף
- `Name` – שם הסניף
- `Address` – כתובת הסניף

### OfficeServices
קישור בין סניפים לסוגי שירותים שהם מספקים.
- `Id` – מזהה ייחודי
- `OfficeId` – מזהה הסניף
- `ServiceTypeId` – מזהה סוג השירות

### AppointmentSlots
טבלת תורים זמינים.
- `Id` – מזהה ייחודי
- `OfficeServiceId` – מזהה השירות בסניף
- `SlotStartUtc` – זמן התחלת התור (UTC)
- `SlotEndUtc` – זמן סיום התור (UTC)
- `IsAvailable` – אם התור פנוי

### Users
טבלת משתמשים.
- `Id` – מזהה ייחודי
- `FullName` – שם מלא
- `Email` – כתובת דוא"ל
- `Phone` – מספר טלפון
- `Role` – סוג משתמש (אדם, מנהל משרד וכו')

### Appointments
טבלת תורים שנקבעו.
- `Id` – מזהה ייחודי של תור
- `UserId` – מזהה המשתמש
- `OfficeId` – מזהה הסניף
- `ServiceTypeId` – מזהה סוג השירות
- `SlotStartUtc` / `SlotEndUtc` – זמן התור
- `StatusId` – מזהה סטטוס התור
- `CreatedAt` / `UpdatedAt` – תאריכי יצירה ועדכון
- `CorrelationId` – מזהה ייחודי להקשר
- `Metadata` – אובייקט גמיש לנתונים נוספים

### AuditEventTypes
טבלת סוגי פעולות במערכת.
- `Id` – מזהה ייחודי של סוג פעולה
- `Name` – שם הפעולה (למשל: יצירת תור, ביטול תור)
- `Description` – תיאור הפעולה

### AuditLogs
טבלת לוגים של פעולות שנעשו.
- `Id` – מזהה ייחודי של הלוג
- `TypeId` – מזהה סוג הפעולה (AuditEventTypes)
- `UserId` – מזהה המשתמש שביצע את הפעולה
- `AppointmentId` – מזהה התור (אם רלוונטי)
- `Timestamp` – זמן ביצוע הפעולה
- `Metadata` – אובייקט גמיש, כולל אם הפעולה בוצעה ע"י משתמש או משרד

---
## 📂 מבנה הפרויקט

- **Controllers** – מכילים את נקודות הקצה (Endpoints) של ה־API.
- **Services** – לוגיקה עסקית, חיבור למאגרי הנתונים.
- **data** – הקשר ל־MongoDB (`MongoDbContext`) והגדרות בסיסיות.
   – קובץ להכנסת נתונים התחלתיים (Initial Data) למסד הנתונים.
- **Models** – מחלקות המייצגות ישויות כמו `Appointment`, `User`, `Office` וכו'.
- **Contracts** – אינטרפייסים של שירותים לצורך הזרקה ותלות הפוכה.

---

## ⚙️ דרישות מערכת

- .NET 6 או 7
- MongoDB 8.x+
- Swagger UI להצגת ה־API והדוקומנטציה
- Windows / Linux / MacOS

---

## 🔧 התקנה והרצה

1. התקנת חבילות NuGet:
    ```bash
    dotnet add package MongoDB.Driver
    dotnet add package Swashbuckle.AspNetCore
    ```
2. הגדרת `appsettings.json`:
    ```json
    {
      "MongoDbSettings": {
        "ConnectionString": "mongodb://localhost:27017",
        "DatabaseName": "GovAppointments"
      }
    }
    ```
3. הרצת הפרויקט:
    ```bash
    dotnet run
    ```
4. פתיחת Swagger בדפדפן:
    ```
    https://localhost:7011/index.html
    ```

---

## 🗂️ ישויות עיקריות

### Appointment
- `Id` – מזהה ייחודי
- `UserId` – מזהה המשתמש
- `OfficeId` – מזהה המשרד
- `ServiceTypeId` – סוג השירות
- `SlotStartUtc` / `SlotEndUtc` – זמן התחלה וסיום
- `StatusId` – סטטוס התור
- `CreatedAt` / `UpdatedAt`
- `CorrelationId` – מזהה ייחודי לאירוע
- `Metadata` – מידע נוסף מותאם אישית

### User
- פרטי המשתמש המערכת

### Office / ServiceType / Ministry / OfficeService
- פרטי משרדים, שירותים וסוגי שירותים

### AppointmentSlot
- פרטי זמנים פנויים

### AuditLog
- לוג פעולות מערכת

---

## 🛠️ פונקציות קיימות

- **AppointmentService**
  - יצירת תורים
  - עדכון / ביטול תורים
  - בדיקת זמינות תורים
- **AuditService**
  - רישום פעולות משתמש במערכת
- **BookingProcessService**
  -ExecuteBookingProcessAsync
  --ניהול תהליך הזמנת התור והלוגיקה העסקית
  --שימוש במיקרו סרוויסים עצמאיים 
  --התהליך מתחיל אחרי קביעת סוג הפגישה מיקום וקבלת פרטי המשתמש
  --בשלב ראשון נבדוק אם המשתמש קיים אם לא נוסיף אותו
  --נבדוק זמינות של התור המבוקש בצורה אטומית, תוך חישוב מספר המוזמנים לשעה הדרושה
  -- אם התור פנוי נקבע פגישה עם סטטוס מתאים
  --נרשום לוג לתיעוד
  --נחזיר את הפגישה שנקבעה

- **UserService**
  - ניהול משתמשים (יצירה, עדכון, מחיקה)
- **SlotService**
  - ניהול זמני פגישות פנויים למשרדים
  --GetAppointmentsForDayAndServAsync
  --בדיקת פגישות שנקבעו לסוג שירות ומשרד בתאריך מסויים
  --השירות מחזיר את כל הפגישות הקימות

---

## 📝 קובץ SEED

- נמצא בתיקיית `data/Seed`.
- אחראי על הכנסת נתונים התחלתיים למסד הנתונים (למשל: משרדים, סוגי שירותים, משתמשים לדמו).
- מומלץ להריץ אותו פעם אחת לאחר יצירת מסד הנתונים כדי שיהיו נתונים זמינים לפיתוח ובדיקות.

---

## 📈 תהליכים אפשריים להרחבה

- אימות משתמשים באמצעות JWT / OAuth2
- התראות דוא"ל או SMS על תורים
- ממשק Frontend אינטראקטיבי עם React / Angular
- דוחות סטטיסטיים על תורים ומשרדים
- ניהול הרשאות ושכבות אבטחה מורכבות
- שימור היסטוריית ביטולים ושינויים
- תמיכה במערכת מול מספר מסדי נתונים בו זמנית (multi-tenant)

---

## 🔑 דוקומנטציה

כל נקודת קצה זמינה ב‑Swagger UI, כולל דוגמאות בקשות ותשובות JSON.

---

## ⚡ Notes

- המערכת משתמשת בהזרקת תלויות (DI) לכל השירותים.
- MongoDB אינו מאופשר עם Authentication כברירת מחדל (יש להוסיף במידת הצורך).
- כל שירות ממופה ל־Collection ייחודי במסד הנתונים.


מבנה הפרויקט:
/test/GovAppointmentAPI
│
├── README.md                <-- קובץ תיעוד מרכזי
├── GovAppointmentAPI.sln    <-- קובץ הסולושין
│
├── /Controllers             <-- כל ה-API Controllers
│   ├── AppointmentController.cs
│   └── ...
│
├── /Services                <-- שירותים עסקיים
│   ├── AppointmentService.cs
│   ├── AuditService.cs
│   └── ...
│
├── /Data                    <-- Data Access ו-Seed
│   ├── MongoDbContext.cs
│   └── Seed.cs              <-- קובץ SEED להכנסת נתונים התחלתיים
│
├── /Models                  <-- מודלים של הנתונים
│   ├── Appointment.cs
│   ├── User.cs
│   └── ...
│
│
├── appsettings.json         <-- קובץ קונפיגורציה
└── Program.cs               <-- הקונפיגורציה הראשית של ASP.NET Core


# GovAppointmentAPI

...

## דוגמאות שימוש ב-API

### יצירת תור (Booking)

```bash
curl -X 'POST' \
  'https://localhost:7011/api/Booking/book' \
  -H 'accept: */*' \
  -H 'Content-Type: application/json' \
  -d '{
  "externalUserId": "SSO-ADMIN2",
  "slotId": "slot-office-1-svc-1-2025-11-27T07:00:00.000Z",
  "name": "aaaa",
  "phone": "0556883645",
  "email": "rerere@gmail.com"
}'
###

### קבלת תורים קיימים לפי סוג שירות וסניף(Appointments)

```bash
curl -X 'GET' \
  'https://localhost:7011/api/Appointment?serviceTypeId=svc-1&officeId=office-1&date=2025-11-25' \
  -H 'accept: text/plain'
  ###
