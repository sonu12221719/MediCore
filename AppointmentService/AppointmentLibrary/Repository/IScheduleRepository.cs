using System;
using AppointmentLibrary.Entities;

namespace AppointmentLibrary.Repository;

public interface IScheduleRepository
{
    Task<Schedule?> GetByIdAsync(Guid scheduleId);
    Task<Schedule?> GetSlotAsync(string doctorId, DateOnly date, TimeOnly timeSlot);
    Task<IEnumerable<Schedule>> GetAvailableSlotsByDoctorAsync(string doctorId, DateOnly date);
    Task AddAsync(Schedule schedule);
    Task UpdateAsync(Schedule schedule);
}
