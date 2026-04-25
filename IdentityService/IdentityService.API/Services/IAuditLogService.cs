using System;

namespace IdentityService.API.Services;

public interface IAuditLogService
{
    Task LogAsync(string userId, string action, string resource);
}
