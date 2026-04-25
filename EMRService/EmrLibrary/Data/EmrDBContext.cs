using System;
using EmrLibrary.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmrLibrary.Data;

public class EmrDBContext:DbContext
{
    public EmrDBContext(){}
    public EmrDBContext(DbContextOptions<EmrDBContext>options):base(options){}
    public virtual DbSet<EMR> EMRs {get;set;}
    public virtual DbSet<Prescription> Prescriptions {get;set;}
    public virtual DbSet<TreatmentLog> TreatmentLogs {get;set;}
}
