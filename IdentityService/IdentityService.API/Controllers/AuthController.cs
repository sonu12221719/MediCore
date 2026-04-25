using IdentityService.API.DTOs;
using IdentityService.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IdentityService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IAuditLogService _auditLogService;
        public AuthController(IAuthService authService, IAuditLogService auditLogService)
        {
            _authService = authService;
            _auditLogService = auditLogService;
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                var (UserId,result) = await _authService.LoginAsync(dto);
                await _auditLogService.LogAsync(
                        userId: UserId,
                        action: "Login",
                        resource: "User"
                    );
                return Ok(result);
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Api Endpoint for Refresh token
        /// </summary>
        /// <param name="dto"></param>
        /// <returns>Returns new Access token as a response</returns>
        [HttpPost("refresh-token")]
        [ProducesResponseType(typeof(string),StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(LoginResponseDto),StatusCodes.Status200OK)]
        public async Task<IActionResult> RefreshToken(LoginResponseDto dto)
        {
            try
            {
                var result = await _authService.RefreshTokenAsync(dto.RefreshToken);
                return Ok(result);  
            }
            catch (Exception ex)
            {
                return Unauthorized(ex.Message);
            }
        }
    }
}
