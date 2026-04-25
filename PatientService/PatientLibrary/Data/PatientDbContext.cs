using System;
using Microsoft.EntityFrameworkCore;
using PatientLibrary.Entities;

namespace PatientLibrary.Data;

public class PatientDbContext:DbContext
{
    public PatientDbContext(){}
    public PatientDbContext(DbContextOptions<PatientDbContext>options):base(options){}

    public virtual DbSet<Patient> Patients { get; set; }
    public virtual DbSet<PatientDocument> PatientDocuments { get; set; }
}
