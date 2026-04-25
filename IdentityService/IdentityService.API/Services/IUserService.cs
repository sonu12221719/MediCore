using IdentityService.API.DTOs;
using IdentityServiceLibrary.Entities;

namespace IdentityService.API.Services
{
    public interface IUserService
    {
        /// <summary>
        ///     Registers a new user in the database.
        /// </summary>
        /// <param name="user">The user object containing the details to be registered.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<UserResponseDto> RegisterUserAsync(RegisterRequestDto dto);

        /// <summary>
        ///     Retrieves all users from the database.
        /// </summary>
        /// <returns>A collection of all users, or an empty collection if none found.</returns>
        Task<IEnumerable<UserResponseDto>> GetAllUserAsync();

        /// <summary>
        ///     Retrieves a user from the database by their unique identifier.
        /// </summary>
        /// <param name="userId">The unique identifier of the user.</param>
        /// <returns>The user if found, otherwise null.</returns>
        Task<UserResponseDto?> GetByIdAsync(string userId);

        /// <summary>
        ///     Updates an existing user in the database.
        /// </summary>
        /// <param name="userId">The unique identifier of the user to update.</param>
        /// <param name="user">The updated user object.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task UpdateUserAsync(string userId, UserUpdateDto dto);

        /// <summary>
        ///     Deletes an existing user from the database by their unique identifier.
        /// </summary>
        /// <param name="userId">The unique identifier of the user to delete.</param>
        /// <returns>
        ///     A <see cref="Task{TResult}"/> representing the asynchronous operation.
        ///     Returns <c>true</c> if deleted successfully, <c>false</c> if user not found.
        /// </returns>
        Task DeleteUserAsync(string userId);
    }
}
