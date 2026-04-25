using LabService.API.DTOs;
using LabService.API.Services;
using LabServiceLibrary.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LabService.API.Controllers
{
    [ApiController]
    [Route("api/lab")]
    [Authorize]
    public class LabController : ControllerBase
    {
        private readonly ILabService _labService;

        public LabController(ILabService labService)
        {
            _labService = labService;
        }

        // POST /api/lab/tests
        [HttpPost("tests")]
        [Authorize(Roles = "Doctor")]
        public async Task<IActionResult> CreateTest([FromBody] CreateLabTestRequestDto dto)
        {
            try
            {
                var result = await _labService.CreateTestAsync(dto);
                return CreatedAtAction(nameof(GetTestById), new { id = result.TestID }, result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        // GET /api/lab/tests
        [HttpGet("tests")]
        [Authorize(Roles = "Doctor,Admin")]
        public async Task<IActionResult> GetAllTests()
        {
            try
            {
                var result = await _labService.GetAllTestsAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        // GET /api/lab/tests/{id}
        [HttpGet("tests/{id:guid}")]
        [Authorize(Roles = "Doctor,Admin,LabTechnician,Patient")]
        public async Task<IActionResult> GetTestById(Guid id)
        {
            try
            {
                var result = await _labService.GetTestByIdAsync(id);
                return Ok(result);
            }
            catch (LabTestNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        // GET /api/lab/tests/patient/{patientId}
        [HttpGet("tests/patient/{patientId}")]
        [Authorize(Roles = "Doctor,Admin")]
        public async Task<IActionResult> GetTestsByPatient(string patientId)
        {
            try
            {
                var result = await _labService.GetTestsByPatientAsync(patientId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        // PUT /api/lab/tests/{id}/assign
        [HttpPut("tests/{id:guid}/assign")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AssignTechnician(Guid id, [FromBody] AssignTechnicianRequestDto dto)
        {
            try
            {
                var result = await _labService.AssignTechnicianAsync(id, dto);
                return Ok(result);
            }
            catch (LabTestNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (LabServiceException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        // PUT /api/lab/tests/{id}/status
        [HttpPut("tests/{id:guid}/status")]
        [Authorize(Roles = "LabTechnician")]
        public async Task<IActionResult> UpdateTestStatus(Guid id, [FromBody] UpdateLabTestStatusRequestDto dto)
        {
            try
            {
                var result = await _labService.UpdateTestStatusAsync(id, dto);
                return Ok(result);
            }
            catch (LabTestNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (UnauthorizedLabAccessException)
            {
                return Forbid();
            }
            catch (LabServiceException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        // POST /api/lab/tests/{id}/report
        [HttpPost("tests/{id:guid}/report")]
        [Authorize(Roles = "LabTechnician")]
        public async Task<IActionResult> UploadReport(Guid id, [FromForm] UploadLabReportRequestDto dto)
        {
            try
            {
                var result = await _labService.UploadReportAsync(id, dto);
                return Created(string.Empty, result);
            }
            catch (LabTestNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (UnauthorizedLabAccessException)
            {
                return Forbid();
            }
            catch (LabReportAlreadyExistsException ex)
            {
                return Conflict(new { message = ex.Message });
            }
            catch (LabServiceException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        // GET /api/lab/tests/{id}/report
        [HttpGet("tests/{id:guid}/report")]
        [Authorize(Roles = "Doctor,Admin,LabTechnician,Patient")]
        public async Task<IActionResult> GetReport(Guid id)
        {
            try
            {
                var result = await _labService.GetReportAsync(id);
                return Ok(result);
            }
            catch (LabTestNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (LabReportNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }
}
