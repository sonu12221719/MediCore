using System;
using AutoMapper;
using Emr.API.DTOs;
using EmrLibrary.Entities;
using EmrLibrary.Enums;

namespace Emr.API.Mapper;

public class MappingProfile:Profile
{
    public MappingProfile()
    {
        // EMR
        CreateMap<CreateEMRRequestDto, EMR>()
            .ForMember(dest => dest.EMRID,     opt => opt.MapFrom(_ => Guid.NewGuid()))
            .ForMember(dest => dest.Status,    opt => opt.MapFrom(_ => EMRStatus.Active))
            .ForMember(dest => dest.Date,      opt => opt.MapFrom(_ => DateTime.UtcNow))
            .ForMember(dest => dest.DoctorID,  opt => opt.Ignore())   // set from token
            .ForMember(dest => dest.Prescriptions,  opt => opt.Ignore())
            .ForMember(dest => dest.TreatmentLogs,  opt => opt.Ignore());

        CreateMap<EMR, EMRResponseDto>()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));

        // Prescription
        CreateMap<AddPrescriptionRequestDto, Prescription>()
            .ForMember(dest => dest.PrescriptionID, opt => opt.MapFrom(_ => Guid.NewGuid()))
            .ForMember(dest => dest.Status,         opt => opt.MapFrom(_ => PrescriptionStatus.Issued))
            .ForMember(dest => dest.CreatedAt,      opt => opt.MapFrom(_ => DateTime.UtcNow))
            .ForMember(dest => dest.DoctorID,       opt => opt.Ignore())   // set from token
            .ForMember(dest => dest.EMRID,          opt => opt.Ignore())   // set in service
            .ForMember(dest => dest.EMR,            opt => opt.Ignore());

        CreateMap<Prescription, PrescriptionResponseDto>()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));

        // TreatmentLog
        CreateMap<AddTreatmentLogRequestDto, TreatmentLog>()
            .ForMember(dest => dest.LogID,   opt => opt.MapFrom(_ => Guid.NewGuid()))
            .ForMember(dest => dest.Status,  opt => opt.MapFrom(_ => TreatmentLogStatus.Recorded))
            .ForMember(dest => dest.Date,    opt => opt.MapFrom(_ => DateTime.UtcNow))
            .ForMember(dest => dest.NurseID, opt => opt.Ignore())   // set from token
            .ForMember(dest => dest.EMRID,   opt => opt.Ignore())   // set in service
            .ForMember(dest => dest.EMR,     opt => opt.Ignore());

        CreateMap<TreatmentLog, TreatmentLogResponseDto>()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));
    
    }
}
