using System;
using AppointmentService.API.DTOs;

namespace AppointmentService.API.Services;

public interface IAppointmentService
{
    Task<AppointmentResponseDto> BookAsync(BookAppointmentRequestDto dto);
    Task<IEnumerable<AppointmentResponseDto>> GetMyAppointmentsAsync();
    Task<AppointmentResponseDto?> GetByIdAsync(Guid appointmentId);
    Task<bool> RescheduleAsync(Guid appointmentId, RescheduleRequestDto dto);
    Task<bool> CancelAsync(Guid appointmentId);
    Task<bool> CompleteAsync(Guid appointmentId);
}   
