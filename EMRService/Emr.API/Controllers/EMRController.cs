using Emr.API.DTOs;
using Emr.API.Services;
using EmrLibrary.Enums;
using EmrLibrary.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Emr.API.Controllers
{
    [ApiController]
    [Route("api/emr")]
    [Authorize]
    public class EMRController : ControllerBase
    {
        private readonly IEMRService _emrService;

        public EMRController(IEMRService emrService)
        {
            _emrService = emrService;
        }

        // POST /api/emr
        [HttpPost]
        [Authorize(Roles = "Doctor")]
        public async Task<IActionResult> Create([FromBody] CreateEMRRequestDto dto)
        {
            try
            {
                var result = await _emrService.CreateAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = result.EMRID }, result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        // GET /api/emr/{id}
        [HttpGet("{id:guid}")]
        [Authorize(Roles = "Doctor,Nurse,Admin,Patient")]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var result = await _emrService.GetByIdAsync(id);
                return Ok(result);
            }
            catch (EMRNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        // GET /api/emr/patient/{patientId}
        [HttpGet("patient/{patientId}")]
        [Authorize(Roles = "Doctor,Nurse,Admin")]
        public async Task<IActionResult> GetByPatientId(string patientId)
        {
            try
            {
                var result = await _emrService.GetByPatientIdAsync(patientId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        // PUT /api/emr/{id}
        [HttpPut("{id:guid}")]
        [Authorize(Roles = "Doctor")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateEMRRequestDto dto)
        {
            try
            {
                var result = await _emrService.UpdateAsync(id, dto);
                return Ok(result);
            }
            catch (EMRNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (UnauthorizedEMRAccessException ex)
            {
                return Forbid();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        // POST /api/emr/{id}/prescriptions
        [HttpPost("{id:guid}/prescriptions")]
        [Authorize(Roles = "Doctor")]
        public async Task<IActionResult> AddPrescription(Guid id, [FromBody] AddPrescriptionRequestDto dto)
        {
            try
            {
                var result = await _emrService.AddPrescriptionAsync(id, dto);
                return Created(string.Empty, result);
            }
            catch (EMRNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (EmrServiceException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        // GET /api/emr/{id}/prescriptions
        [HttpGet("{id:guid}/prescriptions")]
        [Authorize(Roles = "Doctor,Nurse,Admin,Patient")]
        public async Task<IActionResult> GetPrescriptions(Guid id)
        {
            try
            {
                var result = await _emrService.GetPrescriptionsAsync(id);
                return Ok(result);
            }
            catch (EMRNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        // POST /api/emr/{id}/logs
        [HttpPost("{id:guid}/logs")]
        [Authorize(Roles = "Nurse")]
        public async Task<IActionResult> AddTreatmentLog(Guid id, [FromBody] AddTreatmentLogRequestDto dto)
        {
            try
            {
                var result = await _emrService.AddTreatmentLogAsync(id, dto);
                return Created(string.Empty, result);
            }
            catch (EMRNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (EmrServiceException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        // GET /api/emr/{id}/logs
        [HttpGet("{id:guid}/logs")]
        [Authorize(Roles =$"{nameof(RoleOption.Admin)},{nameof(RoleOption.Doctor)},{nameof(RoleOption.Nurse)}")]
        public async Task<IActionResult> GetTreatmentLogs(Guid id)
        {
            try
            {
                var result = await _emrService.GetTreatmentLogsAsync(id);
                return Ok(result);
            }
            catch (EMRNotFoundException ex)
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
