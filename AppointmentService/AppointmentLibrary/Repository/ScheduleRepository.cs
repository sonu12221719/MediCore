using System;
using AppointmentLibrary.Data;
using AppointmentLibrary.Entities;
using AppointmentLibrary.Enums;
using Microsoft.EntityFrameworkCore;

namespace AppointmentLibrary.Repository;

public class ScheduleRepository:IScheduleRepository
{
    private readonly AppointmentDBContext _context;

    public ScheduleRepository(AppointmentDBContext context)
    {
        _context = context;
    }

    public async Task<Schedule?> GetByIdAsync(Guid scheduleId)
    {
        return await _context.Schedules
            .FirstOrDefaultAsync(s => s.ScheduleID == scheduleId);
    }

    // Used before booking — check if this exact slot exists and is available
    public async Task<Schedule?> GetSlotAsync(string doctorId, DateOnly date, TimeOnly timeSlot)
    {
        return await _context.Schedules
            .FirstOrDefaultAsync(s =>
                s.DoctorID     == doctorId  &&
                s.Date         == date      &&
                s.TimeSlot     == timeSlot  &&
                s.Availability == AvailabilityStatus.Available);
    }

    // Used by patient to see available slots for a doctor on a date
    public async Task<IEnumerable<Schedule>> GetAvailableSlotsByDoctorAsync(string doctorId, DateOnly date)
    {
        return await _context.Schedules
            .Where(s =>
                s.DoctorID     == doctorId               &&
                s.Date         == date                   &&
                s.Availability == AvailabilityStatus.Available)
            .OrderBy(s => s.TimeSlot)
            .ToListAsync();
    }

    public async Task AddAsync(Schedule schedule)
    {
        await _context.Schedules.AddAsync(schedule);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Schedule schedule)
    {
        schedule.UpdatedAt = DateTime.UtcNow;
        _context.Schedules.Update(schedule);
        await _context.SaveChangesAsync();
    }
}
