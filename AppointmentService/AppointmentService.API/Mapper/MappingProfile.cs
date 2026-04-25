using System;
using AppointmentLibrary.Entities;
using AppointmentLibrary.Enums;
using AppointmentService.API.DTOs;
using AutoMapper;

namespace AppointmentService.API.Mapper;

public class MappingProfile:Profile
{
    public MappingProfile()
    {
        // Appointment
        CreateMap<BookAppointmentRequestDto, Appointment>()
            .ForMember(dest => dest.AppointmentID, opt => opt.MapFrom(_ => Guid.NewGuid()))
            .ForMember(dest => dest.Status,        opt => opt.MapFrom(_ => AppointmentStatus.Pending))
            .ForMember(dest => dest.CreatedAt,     opt => opt.MapFrom(_ => DateTime.UtcNow))
            .ForMember(dest => dest.PatientID,     opt => opt.Ignore())   // set from token in service
            .ForMember(dest => dest.Schedule,      opt => opt.Ignore());

        CreateMap<Appointment, AppointmentResponseDto>()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));

        // Schedule
        CreateMap<Schedule, ScheduleResponseDto>()
            .ForMember(dest => dest.Availability, opt => opt.MapFrom(src => src.Availability.ToString()));
    }
}
