using System;

namespace Compliance.API.DTOs;

public class CreateAuditRequestDto
{
    public string Scope { get; set; } = string.Empty;
}
