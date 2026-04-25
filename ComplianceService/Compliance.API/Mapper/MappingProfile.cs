using System;
using AutoMapper;
using Compliance.API.DTOs;
using ComplianceLibrary.Entities;
using ComplianceLibrary.Enums;

namespace Compliance.API.Mapper;

public class MappingProfile:Profile
{
    public MappingProfile()
    {
        // ComplianceRecord
        CreateMap<CreateComplianceRecordRequestDto, ComplianceRecord>()
            .ForMember(dest => dest.ComplianceID, opt => opt.MapFrom(_ => Guid.NewGuid()))
            .ForMember(dest => dest.Result,       opt => opt.MapFrom(_ => ComplianceResult.Pending))
            .ForMember(dest => dest.Date,         opt => opt.MapFrom(_ => DateTime.UtcNow));

        CreateMap<ComplianceRecord, ComplianceRecordResponseDto>()
            .ForMember(dest => dest.Type,   opt => opt.MapFrom(src => src.Type.ToString()))
            .ForMember(dest => dest.Result, opt => opt.MapFrom(src => src.Result.ToString()));

        // Audit
        CreateMap<CreateAuditRequestDto, Audit>()
            .ForMember(dest => dest.AuditID,  opt => opt.MapFrom(_ => Guid.NewGuid()))
            .ForMember(dest => dest.Status,   opt => opt.MapFrom(_ => AuditStatus.Scheduled))
            .ForMember(dest => dest.Date,     opt => opt.MapFrom(_ => DateTime.UtcNow))
            .ForMember(dest => dest.AdminID,  opt => opt.Ignore())   // set from token
            .ForMember(dest => dest.Findings, opt => opt.Ignore());

        CreateMap<Audit, AuditResponseDto>()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));
    }
}
