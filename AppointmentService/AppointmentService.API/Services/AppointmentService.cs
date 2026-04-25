using System;
using System.Security.Claims;
using AppointmentLibrary.Entities;
using AppointmentLibrary.Enums;
using AppointmentLibrary.Repository;
using AppointmentService.API.DTOs;
using AutoMapper;

namespace AppointmentService.API.Services;

public class AppointmentService : IAppointmentService
{
    private readonly IAppointmentRepository _appointmentRepository;
    private readonly IScheduleRepository _scheduleRepository;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AppointmentService(
        IAppointmentRepository appointmentRepository,
        IScheduleRepository scheduleRepository,
        IMapper mapper,
        IHttpContextAccessor httpContextAccessor)
    {
        _appointmentRepository = appointmentRepository;
        _scheduleRepository    = scheduleRepository;
        _mapper                = mapper;
        _httpContextAccessor   = httpContextAccessor;
    }

    private string GetUserIdFromToken()
    {
        return _httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.NameIdentifier)!;
    }

    public async Task<AppointmentResponseDto> BookAsync(BookAppointmentRequestDto dto)
    {
        // 1. Check slot is available
        var slot = await _scheduleRepository.GetSlotAsync(dto.DoctorID, dto.Date, dto.Time);
        if (slot is null)
            throw new InvalidOperationException("Selected slot is not available.");

        // 2. Map DTO to entity and set PatientID from token
        var appointment = _mapper.Map<Appointment>(dto);
        appointment.PatientID = GetUserIdFromToken();

        // 3. Save appointment
        await _appointmentRepository.AddAsync(appointment);

        // 4. Mark slot as booked
        slot.Availability = AppointmentLibrary.Enums.AvailabilityStatus.Booked;
        await _scheduleRepository.UpdateAsync(slot);

        return _mapper.Map<AppointmentResponseDto>(appointment);
    }

    public async Task<IEnumerable<AppointmentResponseDto>> GetMyAppointmentsAsync()
    {
        var patientId   = GetUserIdFromToken();
        var appointments = await _appointmentRepository.GetByPatientIdAsync(patientId);
        return _mapper.Map<IEnumerable<AppointmentResponseDto>>(appointments);
    }

    public async Task<AppointmentResponseDto?> GetByIdAsync(Guid appointmentId)
    {
        var appointment = await _appointmentRepository.GetByIdAsync(appointmentId);
        if (appointment is null) return null;
        return _mapper.Map<AppointmentResponseDto>(appointment);
    }

    public async Task<bool> RescheduleAsync(Guid appointmentId, RescheduleRequestDto dto)
    {
        var appointment = await _appointmentRepository.GetByIdAsync(appointmentId);
        if (appointment is null) return false;

        if (appointment.PatientID != GetUserIdFromToken())
            throw new UnauthorizedAccessException("You can only reschedule your own appointments.");

        // 1. Free old slot
        var oldSlot = await _scheduleRepository.GetSlotAsync(appointment.DoctorID, appointment.Date, appointment.Time);
        if (oldSlot is not null)
        {
            oldSlot.Availability = AvailabilityStatus.Available;
            await _scheduleRepository.UpdateAsync(oldSlot);
        }

        // 2. Check new slot is available
        var newSlot = await _scheduleRepository.GetSlotAsync(appointment.DoctorID, dto.NewDate, dto.NewTime);
        if (newSlot is null)
            throw new InvalidOperationException("New slot is not available.");

        // 3. Update appointment
        appointment.Date   = dto.NewDate;
        appointment.Time   = dto.NewTime;
        appointment.Status = AppointmentStatus.Pending;
        await _appointmentRepository.UpdateAsync(appointment);

        // 4. Mark new slot as booked
        newSlot.Availability = AvailabilityStatus.Booked;
        await _scheduleRepository.UpdateAsync(newSlot);

        return true;
    }

    public async Task<bool> CancelAsync(Guid appointmentId)
    {
        var appointment = await _appointmentRepository.GetByIdAsync(appointmentId);
        if (appointment is null) return false;

        if (appointment.PatientID != GetUserIdFromToken())
            throw new UnauthorizedAccessException("You can only cancel your own appointments.");

        // Free the slot
        var slot = await _scheduleRepository.GetSlotAsync(appointment.DoctorID, appointment.Date, appointment.Time);
        if (slot is not null)
        {
            slot.Availability = AvailabilityStatus.Available;
            await _scheduleRepository.UpdateAsync(slot);
        }

        appointment.Status = AppointmentStatus.Cancelled;
        await _appointmentRepository.UpdateAsync(appointment);
        return true;
    }

    public async Task<bool> CompleteAsync(Guid appointmentId)
    {
        var appointment = await _appointmentRepository.GetByIdAsync(appointmentId);
        if (appointment is null) return false;

        // Only the doctor of this appointment can mark it complete
        if (appointment.DoctorID != GetUserIdFromToken())
            throw new UnauthorizedAccessException("You can only complete your own appointments.");

        appointment.Status = AppointmentStatus.Completed;
        await _appointmentRepository.UpdateAsync(appointment);
        return true;
    }
}
