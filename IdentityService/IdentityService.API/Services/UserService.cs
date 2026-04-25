using AutoMapper;
using BCrypt.Net;
using IdentityService.API.DTOs;
using IdentityServiceLibrary.Entities;
using IdentityServiceLibrary.Enums;
using IdentityServiceLibrary.IdentityException;
using IdentityServiceLibrary.Repository;

namespace IdentityService.API.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;
        public UserService(IUserRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<UserResponseDto> RegisterUserAsync(RegisterRequestDto dto)
        {
            try
            {
                var emailExist = await _repository.GetByEmailAsync(dto.Email);
                if (emailExist!=null)
                {
                    throw new IdentityServiceException("Email already exists.");
                }
                var user = _mapper.Map<User>(dto);
                user.UserId=Guid.NewGuid().ToString()[..4];
                user.Password = BCrypt.Net.BCrypt.HashPassword(dto.Password);
                var result = await _repository.RegisterUserAsync(user);

                return _mapper.Map<User,UserResponseDto>(result);
            }
            catch (AutoMapperMappingException ex)
            {
                throw new IdentityServiceException($"Invalid Role or Status value. {ex.InnerException?.Message}");
            }
            catch (Exception ex)
            {
                throw new IdentityServiceException(ex.Message); // don't hide the real message
            }
        }

        public async Task<IEnumerable<UserResponseDto>> GetAllUserAsync()
        {
            try
            {
                var users = await _repository.GetAllUserAsync();
                var response = _mapper.Map<IEnumerable<UserResponseDto>>(users);
                return response;
            }
            catch
            {
                throw new IdentityServiceException("Error in fetching Users");
            }
        }

        public async Task<UserResponseDto?> GetByIdAsync(string userId)
        {
            
            var user = await _repository.GetByIdAsync(userId);
            if (user == null)
            {
                throw new IdentityServiceException("User not found.");
            }
            var response = _mapper.Map<UserResponseDto>(user);
            return response;
            
        }

        public async Task UpdateUserAsync(string userId, UserUpdateDto dto)
        {
            try
            {
                var user = await _repository.GetByIdAsync(userId);
                if (user == null)
                {
                    throw new IdentityServiceException("User not found.");
                }
                
                user.UserName=dto.UserName;
                user.Role=Enum.Parse<RoleOption>(dto.Role);
                user.Status=Enum.Parse<StatusOption>(dto.Status);
                await _repository.UpdateUserAsync(user);
            }
            catch(IdentityServiceException ex)
            {
                throw new IdentityServiceException(ex.Message);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task DeleteUserAsync(string userId)
        {
            try
            {
                if (userId == null)
                {
                    throw new IdentityServiceException("User id is required.");
                }
                bool isDeleted = await _repository.DeleteUserAsync(userId);
                if (!isDeleted)
                {
                    throw new IdentityServiceException("Failed to delete user.");
                }
            }
            catch(IdentityServiceException ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
