using AppointmentService.API.DTOs;
using AppointmentService.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AppointmentService.API.Controllers
{
    [ApiController]
    [Route("api/schedules")]
    [Authorize]
    public class ScheduleController : ControllerBase
    {
        private readonly IScheduleService _scheduleService;

        public ScheduleController(IScheduleService scheduleService)
        {
            _scheduleService = scheduleService;
        }

        // POST /api/schedules
        [HttpPost]
        [Authorize(Roles = "Doctor,Admin")]
        public async Task<IActionResult> CreateSlots([FromBody] CreateScheduleRequestDto dto)
        {
            try
            {
                await _scheduleService.CreateSlotsAsync(dto);
                return Created();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        // GET /api/schedules/{doctorId}?date=2024-01-01
        [HttpGet("{doctorId}")]
        public async Task<IActionResult> GetAvailableSlots(string doctorId, [FromQuery] DateOnly date)
        {
            try
            {
                var result = await _scheduleService.GetAvailableSlotsAsync(doctorId, date);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }
}
