using System;
using System.Collections.Generic;
using System.Text;
using IdentityServiceLibrary.Entities;

namespace IdentityServiceLibrary.Repository
{
    public interface IUserRepository
    {
        /// <summary>
        ///     Registers a new user in the database.
        /// </summary>
        /// <param name="user">The user object containing the details to be registered.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<User> RegisterUserAsync(User user);

        /// <summary>
        ///     Retreive all users from the database
        /// </summary>
        /// <returns>A collection of all users, or empty collections if none found.</returns>
        Task<IEnumerable<User>> GetAllUserAsync();

        /// <summary>
        ///     Retreive a user from the database by their unique Identifier
        /// </summary>
        /// <param name="UserId">The unique identifier for the user</param>
        /// <returns>The User if found, else null</returns>
        Task<User?> GetByIdAsync(string userId);

        /// <summary>
        /// Retreive a user from the database by their email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        Task<User?> GetByEmailAsync(string email);

        /// <summary>
        ///     Update an existing user from the database
        /// </summary>
        /// <param name="UserId">The unique identifier for the user</param>
        /// <param name="user">The updated user object</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task UpdateUserAsync(User user);

        /// <summary>
        ///     Delete an existing user form the database by unique identifier
        /// </summary>
        /// <param name="userId">The user object</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task<bool> DeleteUserAsync(string userId);

    }
}
