using System;
using Microsoft.EntityFrameworkCore;
using PharmacyLibrary.Entities;

namespace PharmacyLibrary.Data;

public class PharmacyDbContext:DbContext
{
    public PharmacyDbContext(){}
    public PharmacyDbContext(DbContextOptions<PharmacyDbContext>options):base(options){}
    public DbSet<Dispense> Dispenses {get;set;}
    public DbSet<Medicine> Medicines {get;set;}   
}
