using GovAppointmentAPI.models;
using GovAppointmentAPI.Services;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class AppointmentController : ControllerBase
{
    private readonly AppointmentService _service;
    public AppointmentController(AppointmentService service) => _service = service;

//    [HttpGet]
//    public async Task<IActionResult> GetAll() => Ok(await _service.GetAllAsync());

//    [HttpGet("{id}")]
//    public async Task<IActionResult> Get(string id)
//    {
//        var appt = await _service.GetAsync(id);
//        if (appt == null) return NotFound();
//        return Ok(appt);
//    }

//    [HttpPost]
//    public async Task<IActionResult> Create([FromBody] Appointment appt, [FromHeader] string userId)
//    {
//        var created = await _service.CreateAsync(appt, userId);
//        return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
//    }

//    [HttpPost("{id}/cancel")]
//    public async Task<IActionResult> Cancel(string id, [FromHeader] string userId, [FromQuery] bool byOffice = false)
//    {
//        var cancelled = await _service.CancelAsync(id, userId, byOffice);
//        if (cancelled == null) return NotFound();
//        return Ok(cancelled);
 //   }
}
