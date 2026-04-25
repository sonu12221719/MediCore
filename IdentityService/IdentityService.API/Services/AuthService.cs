using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using IdentityService.API.DTOs;
using IdentityService.API.Utilities;
using IdentityServiceLibrary.Entities;
using IdentityServiceLibrary.IdentityException;
using IdentityServiceLibrary.Repository;
using Microsoft.IdentityModel.Tokens;

namespace IdentityService.API.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;
        private readonly ITokenRepository _tokenRepository;
        public AuthService(IUserRepository userRepository, IConfiguration configuration, ITokenRepository tokenRepository)
        {
            _userRepository = userRepository;
            _configuration = configuration;
            _tokenRepository=tokenRepository;
        }
        public async Task<(string UserId,LoginResponseDto result)> LoginAsync(LoginDto dto)
        {
            var user = await _userRepository.GetByEmailAsync(dto.Email);
            if (user==null)
            {
                throw new IdentityServiceException("Invalid Email");
            }
            bool ValidatePassword = BCrypt.Net.BCrypt.Verify(dto.Password, user.Password);
            if (!ValidatePassword)
            {
                throw new IdentityServiceException("Password is invalid.");
            }

            string accessToken = GenerateAccessToken(user);
            string refreshToken = GenerateRefreshToken();
            var refreshTokenEntity = new RefreshToken
            {
                Token=refreshToken,
                ExpiryDate=DateTime.UtcNow.AddDays(7),
                IsRevoked=false,
                UserId=user.UserId
            };
            await _tokenRepository.AddRefreshTokenAsync(refreshTokenEntity);
            var response = new LoginResponseDto
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                LoginAt = DateTime.UtcNow
            };
            return (user.UserId, response);
        }

        public async Task<LoginResponseDto> RefreshTokenAsync(string refreshToken)
        {
            var storedToken = await _tokenRepository.GetRefreshTokenAsync(refreshToken);
            if(storedToken==null || storedToken.IsRevoked || storedToken.ExpiryDate < DateTime.UtcNow)
            {
                throw new Exception(Messages.InvalidRefreshToken);
            }
            var user = await _userRepository.GetByIdAsync(storedToken.UserId);
            if (user == null)
            {
                throw new IdentityServiceException("User not found.");
            }
            var newAccessToken = GenerateAccessToken(user);
            storedToken.IsRevoked=true;
            var newRefreshToken=GenerateRefreshToken();
            await _tokenRepository.AddRefreshTokenAsync(new RefreshToken
            {
                Token=newRefreshToken,
                ExpiryDate=DateTime.UtcNow.AddDays(7),
                UserId=storedToken.UserId
            });

            return new LoginResponseDto
            {
                AccessToken=newAccessToken,
                RefreshToken=newRefreshToken,
                LoginAt = DateTime.UtcNow
            };
        }

        private string GenerateAccessToken(User user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserId),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                    issuer: _configuration["Jwt:Issuer"],
                    audience: _configuration["Jwt:Audience"],
                    claims: claims,
                    expires: DateTime.UtcNow.AddMinutes(int.Parse(_configuration["Jwt:ExpiryMinutes"])),
                    signingCredentials: creds
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string GenerateRefreshToken()
        {
            var randomBytes = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomBytes);
            return Convert.ToBase64String(randomBytes);
        }
    }
}
