using System;
using LabServiceLibrary.Entities;
using Microsoft.EntityFrameworkCore;

namespace LabServiceLibrary.Data;

public class LabServiceDBContext:DbContext
{
    public LabServiceDBContext(){}
    public LabServiceDBContext(DbContextOptions<LabServiceDBContext>options):base(options){}
    public virtual DbSet<LabTest> LabTests {get;set;}
    public virtual DbSet<LabReport> LabReports {get;set;}

}
