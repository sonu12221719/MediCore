using System;
using IdentityServiceLibrary.Data;
using IdentityServiceLibrary.Entities;
using Microsoft.EntityFrameworkCore;

namespace IdentityServiceLibrary.Repository;

public class TokenRepository : ITokenRepository
{
    private readonly IdentityDBContext _context;
    public TokenRepository(IdentityDBContext context)
    {
        _context=context;
    }
    public async Task AddRefreshTokenAsync(RefreshToken refreshToken)
    {
        await _context.RefreshTokens.AddAsync(refreshToken);
        await _context.SaveChangesAsync();
    }

    public async Task<RefreshToken?> GetRefreshTokenAsync(string token)
    {
        var refreshToken = await _context.RefreshTokens.FirstOrDefaultAsync(x=>x.Token==token);
        if (refreshToken == null)
        {
            return null;
        }
        return refreshToken;
    }
}
