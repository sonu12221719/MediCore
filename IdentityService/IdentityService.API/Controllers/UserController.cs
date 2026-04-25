using IdentityService.API.DTOs;
using IdentityService.API.HttpClientService;
using IdentityService.API.Services;
using IdentityService.API.Utilities;
using IdentityServiceLibrary.Enums;
using IdentityServiceLibrary.IdentityException;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IdentityService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IAuditLogService _auditService;
        private readonly IPatientServiceClient _patientClient;
        private string _userId;
        public UserController(IUserService userService, IAuditLogService auditService, IPatientServiceClient patientClient)
        {
            _userService = userService;
            _auditService = auditService;
            _patientClient = patientClient;
        }

        /// <summary>
        ///     Endpoint for register new User.
        /// </summary>
        /// <param name="dto">An object to create User</param>
        /// <returns>Success message if user created, else error message.</returns>
        [HttpPost("register")]
        [ProducesResponseType(typeof(IEnumerable<UserResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register(RegisterRequestDto dto)
        {
            try
            {
                var result = await _userService.RegisterUserAsync(dto);
                await _auditService.LogAsync(
                    userId: result.UserId,
                    action: "CREATE",
                    resource: "User"
                );

                _userId=result.UserId;

                if (dto.Role == "Patient")
                {
                    var success = await _patientClient.CreatePatientAsync(
                        result.UserId,
                        dto.patientRequestDtos
                    );

                    if (!success)
                    {
                        // Rollback the created user to avoid orphaned accounts
                        await _userService.DeleteUserAsync(result.UserId);
                        return StatusCode(500, "Patient profile creation failed. Please try again.");
                    }
                    
                }

                return Ok(Messages.UserCreated);
            }
            catch (IdentityServiceException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                await _userService.DeleteUserAsync(_userId);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        ///     Endpoint for retreiving all the users from database
        /// </summary>
        /// <returns>A collection of users if exists, else return empty collection.</returns>
        [Authorize(Roles = nameof(RoleOption.Admin))]
        [HttpGet("getall")]
        [ProducesResponseType(typeof(IEnumerable<UserResponseDto>),StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var users = await _userService.GetAllUserAsync();
                return Ok(users);
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        ///     Endpoint to retreive a user with unique identitifier.
        /// </summary>
        /// <param name="userId">A unique identifier of user.</param>
        /// <returns>Object of User in UserResponseDto formate</returns>
        [HttpGet("{userId}")]
        [ProducesResponseType(typeof(IEnumerable<UserResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetOne(string userId)
        {
            try
            {
                var result = await _userService.GetByIdAsync(userId);
                return Ok(result);
            }
            catch(IdentityServiceException ex)
            {
                return BadRequest(ex.Message);
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Endpoint to update user data.
        /// </summary>
        /// <param name="userId">Unique Identifier of user.</param>
        /// <param name="dto">User object for update.</param>
        /// <returns>Success message for update, else error message</returns>
        [HttpPut("{userId}")]
        [ProducesResponseType(typeof(IEnumerable<UserResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update([FromRoute]string userId, [FromBody] UserUpdateDto dto)
        {
            try
            {
                var existingUser = await _userService.GetByIdAsync(userId);
                if (existingUser == null)
                    return NotFound("User not found.");

                var previousRole = existingUser.Role;
                var newRole = dto.Role;

                if (newRole == "Patient")
                {
                    if (previousRole == "Patient")
                    {
                        var success = await _patientClient.UpdatePatientAsync(
                            userId,
                            dto.patientRequestDtos
                        );

                        if (!success)
                        {
                            return StatusCode(500, "Patient profile Updation failed. Please try again.");
                        }   
                    }
                    else
                    {
                        var success = await _patientClient.CreatePatientAsync(
                            userId,
                            dto.patientRequestDtos
                        );

                        if (!success)
                        {
                            // Rollback the created user to avoid orphaned accounts
                            // await _userService.DeleteUserAsync(userId);
                            return StatusCode(500, "Patient profile creation failed. Please try again.");
                        }
                    }
                }
                await _userService.UpdateUserAsync(userId, dto);
                return Ok("User updated successfully.");
            }
            catch(IdentityServiceException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (System.Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Endpoint for remove user from database
        /// </summary>
        /// <param name="userId">A unique identifier for user.</param>
        /// <returns>Success message on delete, else return error message.</returns>
        [HttpDelete("{userId}")]
        [ProducesResponseType(typeof(IEnumerable<UserResponseDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete([FromRoute]string userId)
        {
            try
            {
                await _userService.DeleteUserAsync(userId);
                return Ok("User deleted successfully.");
            }
            catch(IdentityServiceException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (System.Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

    }
}
