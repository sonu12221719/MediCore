using System;
using AutoMapper;
using LabService.API.DTOs;
using LabServiceLibrary.Entities;
using LabServiceLibrary.Enums;

namespace LabService.API.Mapper;

public class MappingProfile:Profile
{
    public MappingProfile()
    {
        // LabTest
        CreateMap<CreateLabTestRequestDto, LabTest>()
            .ForMember(dest => dest.TestID,       opt => opt.MapFrom(_ => Guid.NewGuid()))
            .ForMember(dest => dest.Status,       opt => opt.MapFrom(_ => LabTestStatus.Requested))
            .ForMember(dest => dest.Date,         opt => opt.MapFrom(_ => DateTime.UtcNow))
            .ForMember(dest => dest.DoctorID,     opt => opt.Ignore())   // set from token
            .ForMember(dest => dest.TechnicianID, opt => opt.Ignore())   // set by admin later
            .ForMember(dest => dest.Report,       opt => opt.Ignore());

        CreateMap<LabTest, LabTestResponseDto>()
            .ForMember(dest => dest.Type,   opt => opt.MapFrom(src => src.Type.ToString()))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));

        // LabReport
        CreateMap<LabReport, LabReportResponseDto>()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));
    
    }
}
