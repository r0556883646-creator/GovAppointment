using GovAppointmentAPI.Contracts;
using GovAppointmentAPI.models;
using GovAppointmentAPI.Services;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class AppointmentController : ControllerBase
{
    private readonly IAppointmentService _serviceAppointment;
    public AppointmentController(IAppointmentService service) => _serviceAppointment = service;

    // GET api/appointments?serviceTypeId=svc-1&officeId=office-1&date=2025-11-25
    [HttpGet]
    public async Task<ActionResult<List<Appointment>>> GetAppointments(
        [FromQuery] string serviceTypeId,
        [FromQuery] string officeId,
        [FromQuery] DateTime date)
    {
        if (string.IsNullOrEmpty(serviceTypeId) || string.IsNullOrEmpty(officeId))
        {
            return BadRequest("serviceTypeId and officeId are required.");
        }

        var appointments = await _serviceAppointment.GetAppointmentsAsync(serviceTypeId, officeId, date);
        return Ok(appointments);
    }
}
