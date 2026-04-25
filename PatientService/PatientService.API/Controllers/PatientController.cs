using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PatientLibrary.Enums;
using PatientLibrary.Exceptions;
using PatientService.API.DTOs;
using PatientService.API.Services;

namespace PatientService.API.Controllers
{
    [Authorize(Roles =$"{nameof(RoleOption.Admin)},{nameof(RoleOption.Patient)}")]
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly IPatientService _patientService;
        public PatientController(IPatientService patientService)
        {
            _patientService=patientService;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> AddPatient(RequestPatientDto dto)
        {
            try
            {
                await _patientService.AddPatientAsync(dto);
                return StatusCode(201, new { success = true, message = "Patient created successfully" });
            }
            catch(PatientException ex)
            {
                return Conflict(new { success = false, message = ex.Message });
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        [Authorize(Roles =nameof(RoleOption.Admin))]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                IEnumerable<ResponsePatientDto> patients = await _patientService.GetAllPatientAsync();
                if (!patients.Any())
                    return Ok(new { success = true, message = "No patients found", data = patients });
                return Ok(new { success = true, message = "Patients retrieved successfully", data = patients });
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        [HttpGet("{patientId}")]
        public async Task<IActionResult> GetOne(string patientId)
        {
            try
            {
                var patient = await _patientService.GetByIdAsync(patientId);
                return Ok(new { success = true, message = "Patient retrieved successfully", data = patient });
            }
            catch(PatientException ex)
            {
                return NotFound(new { success = false, message = ex.Message });
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        [HttpDelete("{patientId}")]
        public async Task<IActionResult> Delete(string patientId)
        {
            try
            {
                await _patientService.DeletePatientAsync(patientId);
                return Ok("Patient deleted successfully.");
            }
            catch(PatientException ex)
            {
                return NotFound(new { success = false, message = ex.Message });
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        [HttpPut("{patientId}")]
        public async Task<IActionResult> Update(string patientId, UpdatePatientDto dto)
        {
            try
            {
                await _patientService.UpdateAsync(dto);
                return Ok(new { success = true, message = "Patient updated successfully" });
            }
            catch(PatientException ex)
            {
                return NotFound(new { success = false, message = ex.Message });
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }
    }
}
