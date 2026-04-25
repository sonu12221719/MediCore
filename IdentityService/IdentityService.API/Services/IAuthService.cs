using IdentityService.API.DTOs;

namespace IdentityService.API.Services
{
    public interface IAuthService
    {
        /// <summary>
        /// Method to authenticate user.
        /// </summary>
        /// <param name="dto">A login request object containing email and password</param>
        /// <returns>LoginResponseDto will return on success, else error object.</returns>
        Task<(string UserId, LoginResponseDto result)> LoginAsync(LoginDto dto);
        Task<LoginResponseDto> RefreshTokenAsync(string refreshToken);
    }
}
