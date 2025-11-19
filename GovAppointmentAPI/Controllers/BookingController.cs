using GovAppointmentAPI.Contracts;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
namespace GovAppointmentAPI.Controllers
{



    [ApiController]
    [Route("api/[controller]")]
    public class BookingController : ControllerBase
    {
        private readonly IBookingProcessService _bookingService;

        public BookingController(IBookingProcessService bookingService)
        {
            _bookingService = bookingService;
        }

        /// <summary>
        /// קובע תור למשתמש חיצוני לפי slotId
        /// </summary>
        [HttpPost("book")]
        public async Task<IActionResult> BookAppointment([FromBody] BookAppointmentRequest request)
        {
            if (string.IsNullOrEmpty(request.ExternalUserId) || string.IsNullOrEmpty(request.SlotId))
                return BadRequest("ExternalUserId and SlotId are required.");

            try
            {
                var appointment = await _bookingService.ExecuteBookingProcessAsync(
                    request.ExternalUserId,
                    request.Name,
                    request.Phone,
                    request.Email,
                    request.SlotId
                );

                return Ok(appointment);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }
    }

    /// <summary>
    /// DTO לקבלת בקשה מהקליינט
    /// </summary>
    public class BookAppointmentRequest
    {
        public string ExternalUserId { get; set; } = default!;
        public string SlotId { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string Phone { get; set; } = default!;
        public string Email { get; set; } = default!;

    }
}
