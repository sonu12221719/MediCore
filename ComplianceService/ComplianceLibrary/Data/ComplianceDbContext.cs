using System;
using ComplianceLibrary.Entities;
using Microsoft.EntityFrameworkCore;

namespace ComplianceLibrary.Data;

public class ComplianceDbContext:DbContext
{
    public ComplianceDbContext(){}
    public ComplianceDbContext(DbContextOptions<ComplianceDbContext> options):base(options){}
    public virtual DbSet<Audit> Audits {get;set;}
    public virtual DbSet<ComplianceRecord> ComplianceRecords {get;set;}
}
