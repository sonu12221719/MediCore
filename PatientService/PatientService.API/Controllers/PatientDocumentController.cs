using System.Security.Claims;
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
    public class PatientDocumentController : ControllerBase
    {
        private readonly IPatientDocumentService _patientDocumentService;
        public PatientDocumentController(IPatientDocumentService patientDocumentService)
        {
            _patientDocumentService=patientDocumentService;
        }
        private string GetPatientIdFromToken()
        {
            return User.FindFirst(ClaimTypes.NameIdentifier).Value;
        } 

        [HttpPost]
        public async Task<IActionResult> UploadDocument(RequestPatientDocumentDto dto)
        {
            try
            {
                string patientId = GetPatientIdFromToken();
                await _patientDocumentService.UploadDocumentAsync(patientId, dto);
                return StatusCode(201, new { success = true, message = "Patient Document created successfully" });
            }
            catch(PatientException ex)
            {
                return BadRequest(ex.Message);
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
                var documents = await _patientDocumentService.GetAllDocumentAsync();
                return StatusCode(200, new {success=true, message="Successfully Document data fetched.",data=documents});
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        [HttpGet("{documentId}")]
        public async Task<IActionResult> GetOne(Guid documentId)
        {
            try
            {
                var document  = await _patientDocumentService.GetDocumentByIdAsync(documentId);
                return StatusCode(200, new {success=true, message="Successfully Document data fetched.",data=document});
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

        [HttpDelete("{documentId}")]
        public async Task<IActionResult> Delete(Guid documentId)
        {
            try
            {
                await _patientDocumentService.DeleteDocumentAsync(documentId);
                return StatusCode(202, new{success=true, message="Successfully document deleted."});
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
