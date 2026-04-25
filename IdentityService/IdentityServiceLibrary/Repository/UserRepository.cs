using System;
using System.Collections.Generic;
using System.Text;
using IdentityServiceLibrary.Data;
using IdentityServiceLibrary.Entities;
using IdentityServiceLibrary.IdentityException;
using Microsoft.EntityFrameworkCore;

namespace IdentityServiceLibrary.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly IdentityDBContext _context;
        public UserRepository(IdentityDBContext context)
        {
            _context = context;
        }

        public async Task<User> RegisterUserAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<IEnumerable<User>> GetAllUserAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User?> GetByIdAsync(string userId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserId == userId);
            if (user == null)
            {
                return null;
            }
            return user;
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null)
            {
                return null;
            }
            return user;
        }

        public async Task UpdateUserAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteUserAsync(string userId)
        {
            var user = await GetByIdAsync(userId);
            if (user == null)
            {
                return false;
            }
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
