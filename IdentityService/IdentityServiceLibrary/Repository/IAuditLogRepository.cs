using System;
using IdentityServiceLibrary.Entities;

namespace IdentityServiceLibrary.Repository;

public interface IAuditLogRepository
{
    Task LogAsync(AuditLog log);
}
