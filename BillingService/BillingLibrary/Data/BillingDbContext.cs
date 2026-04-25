using System;
using BillingLibrary.Entities;
using Microsoft.EntityFrameworkCore;

namespace BillingLibrary.Data;

public class BillingDbContext: DbContext
{
    public BillingDbContext(){}
    public BillingDbContext(DbContextOptions<BillingDbContext>options):base(options){}

    public virtual DbSet<Bill> Bills{ get; set; }
    public virtual DbSet<InsuranceClaim> InsuranceClaims { get; set; }
    public virtual DbSet<Payment> Payments { get; set; }
}
