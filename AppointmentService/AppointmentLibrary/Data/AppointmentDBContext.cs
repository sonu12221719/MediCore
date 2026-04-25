using System;
using AppointmentLibrary.Entities;
using Microsoft.EntityFrameworkCore;

namespace AppointmentLibrary.Data;

public class AppointmentDBContext:DbContext
{
    public AppointmentDBContext(){}
    public AppointmentDBContext(DbContextOptions<AppointmentDBContext> options):base(options){}
    public virtual DbSet<Appointment> Appointments {get; set;}
    public virtual DbSet<Schedule> Schedules {get;set;}
}
