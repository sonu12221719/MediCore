using System;
using AppointmentService.API.DTOs;

namespace AppointmentService.API.Services;

public interface IScheduleService
{
    Task CreateSlotsAsync(CreateScheduleRequestDto dto);
    Task<IEnumerable<ScheduleResponseDto>> GetAvailableSlotsAsync(string doctorId, DateOnly date);
}
