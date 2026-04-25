using System;
using AppointmentLibrary.Entities;
using AppointmentLibrary.Enums;
using AppointmentLibrary.Repository;
using AppointmentService.API.DTOs;
using AutoMapper;

namespace AppointmentService.API.Services;

public class Scheduleservice:IScheduleService
{
    private readonly IScheduleRepository _scheduleRepository;
    private readonly IMapper _mapper;

    public Scheduleservice(IScheduleRepository scheduleRepository, IMapper mapper)
    {
        _scheduleRepository = scheduleRepository;
        _mapper             = mapper;
    }

    public async Task CreateSlotsAsync(CreateScheduleRequestDto dto)
    {
        foreach (var timeSlot in dto.TimeSlots)
        {
            var schedule = new Schedule
            {
                ScheduleID   = Guid.NewGuid(),
                DoctorID     = dto.DoctorID,
                Date         = dto.Date,
                TimeSlot     = timeSlot,
                Availability = AvailabilityStatus.Available,
                CreatedAt    = DateTime.UtcNow
            };

            await _scheduleRepository.AddAsync(schedule);
        }
    }

    public async Task<IEnumerable<ScheduleResponseDto>> GetAvailableSlotsAsync(string doctorId, DateOnly date)
    {
        var slots = await _scheduleRepository.GetAvailableSlotsByDoctorAsync(doctorId, date);
        return _mapper.Map<IEnumerable<ScheduleResponseDto>>(slots);
    }
}
