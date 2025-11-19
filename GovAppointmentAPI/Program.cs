

using GovAppointmentAPI.Contracts;
using GovAppointmentAPI.data;
using GovAppointmentAPI.Services;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);
//builder.Services.Configure<MongoDbSettings>(
//    builder.Configuration.GetSection("MongoDbSettings"));
//builder.Services.AddSingleton<IMongoDatabase>(sp =>
//{
//    var client = sp.GetRequiredService<IMongoClient>();
//    return client.GetDatabase("GovAppointmentsDB"); // שם הדאטאבייס שלך
//});
builder.Services.Configure<MongoDbSettings>(
    builder.Configuration.GetSection("MongoDbSettings"));

builder.Services.AddSingleton<MongoDbContext>();
builder.Services.AddScoped<IAppointmentService,AppointmentService>();
builder.Services.AddScoped<IAuditService, AuditService>();
builder.Services.AddScoped<IBookingProcessService, BookingProcessService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ISlotService, SlotService>();





builder.Services.AddControllers();
builder.Services.AddControllersWithViews(); //
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "GovAppointmentAPI v1");
    c.RoutePrefix = string.Empty; // מציג את Swagger בדף הראשי: https://localhost:5001/
});

//app.UseSwaggerUI();
//app.UseDefaultFiles(); // מחפש index.html ב-wwwroot
app.UseStaticFiles();
app.UseAuthorization();
app.MapControllers();
//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{action=Index}/{id?}");
app.Run();
