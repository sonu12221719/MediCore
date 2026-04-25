using System;
using IdentityServiceLibrary.Entities;

namespace IdentityServiceLibrary.Repository;

public interface ITokenRepository
{
    Task AddRefreshTokenAsync(RefreshToken refreshToken);
    Task<RefreshToken?> GetRefreshTokenAsync(string token); 
}
