using System;
using AppointmentLibrary.Data;
using AppointmentLibrary.Entities;
using Microsoft.EntityFrameworkCore;

namespace AppointmentLibrary.Repository;

public class AppointmentRepository:IAppointmentRepository
{
    private readonly AppointmentDBContext _context;

    public AppointmentRepository(AppointmentDBContext context)
    {
        _context = context;
    }

    public async Task<Appointment?> GetByIdAsync(Guid appointmentId)
    {
        return await _context.Appointments
            .Include(a => a.Schedule)
            .FirstOrDefaultAsync(a => a.AppointmentID == appointmentId);
    }

    public async Task<IEnumerable<Appointment>> GetByPatientIdAsync(string patientId)
    {
        return await _context.Appointments
            .Include(a => a.Schedule)
            .Where(a => a.PatientID == patientId)
            .OrderByDescending(a => a.Date)
            .ToListAsync();
    }

    public async Task<IEnumerable<Appointment>> GetByDoctorIdAsync(string doctorId)
    {
        return await _context.Appointments
            .Include(a => a.Schedule)
            .Where(a => a.DoctorID == doctorId)
            .OrderByDescending(a => a.Date)
            .ToListAsync();
    }

    public async Task AddAsync(Appointment appointment)
    {
        await _context.Appointments.AddAsync(appointment);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Appointment appointment)
    {
        appointment.UpdatedAt = DateTime.UtcNow;
        _context.Appointments.Update(appointment);
        await _context.SaveChangesAsync();
    }
}
