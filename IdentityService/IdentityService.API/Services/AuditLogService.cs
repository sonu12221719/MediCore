using System;
using IdentityServiceLibrary.Entities;
using IdentityServiceLibrary.IdentityException;
using IdentityServiceLibrary.Repository;

namespace IdentityService.API.Services;

public class AuditLogService : IAuditLogService
{
    private readonly IAuditLogRepository _auditRepository;
    public AuditLogService(IAuditLogRepository auditRepository)
    {
        _auditRepository=auditRepository;
    }
    public async Task LogAsync(string userId, string action, string resource)
    {
        try
        {
            var log = new AuditLog
            {
                UserId=userId,
                Action=action,
                Resource=resource,
                TimeStamp=DateTime.UtcNow
            };
            await _auditRepository.LogAsync(log);
            }
        catch (Exception ex)
        {
            throw new IdentityServiceException(ex.Message);
        }
    }
}
