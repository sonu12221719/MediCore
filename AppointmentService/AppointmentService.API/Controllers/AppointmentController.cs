using AppointmentService.API.DTOs;
using AppointmentService.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AppointmentService.API.Controllers
{
    [ApiController]
    [Route("api/appointments")]
    [Authorize]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentService _appointmentService;

        public AppointmentController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        // POST /api/appointments
        [HttpPost]
        [Authorize(Roles = "Patient")]
        public async Task<IActionResult> Book([FromBody] BookAppointmentRequestDto dto)
        {
            try
            {
                var result = await _appointmentService.BookAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = result.AppointmentID }, result);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        // GET /api/appointments
        [HttpGet]
        public async Task<IActionResult> GetMyAppointments()
        {
            try
            {
                var result = await _appointmentService.GetMyAppointmentsAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        // GET /api/appointments/{id}
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var result = await _appointmentService.GetByIdAsync(id);
                if (result is null) return NotFound();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        // PUT /api/appointments/{id}/reschedule
        [HttpPut("{id:guid}/reschedule")]
        [Authorize(Roles = "Patient")]
        public async Task<IActionResult> Reschedule(Guid id, [FromBody] RescheduleRequestDto dto)
        {
            try
            {
                var result = await _appointmentService.RescheduleAsync(id, dto);
                if (!result) return NotFound();
                return NoContent();
            }
            catch (UnauthorizedAccessException ex)
            {
                return Forbid();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        // PUT /api/appointments/{id}/cancel
        [HttpPut("{id:guid}/cancel")]
        [Authorize(Roles = "Patient")]
        public async Task<IActionResult> Cancel(Guid id)
        {
            try
            {
                var result = await _appointmentService.CancelAsync(id);
                if (!result) return NotFound();
                return NoContent();
            }
            catch (UnauthorizedAccessException ex)
            {
                return Forbid();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        // PUT /api/appointments/{id}/complete
        [HttpPut("{id:guid}/complete")]
        [Authorize(Roles = "Doctor")]
        public async Task<IActionResult> Complete(Guid id)
        {
            try
            {
                var result = await _appointmentService.CompleteAsync(id);
                if (!result) return NotFound();
                return NoContent();
            }
            catch (UnauthorizedAccessException ex)
            {
                return Forbid();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }
}
