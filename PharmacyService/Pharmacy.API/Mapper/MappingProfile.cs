using System;
using AutoMapper;
using Pharmacy.API.DTOs;
using PharmacyLibrary.Entities;
using PharmacyLibrary.Enums;

namespace Pharmacy.API.Mapper;

public class MappingProfile:Profile
{
    public MappingProfile()
    {
        // Medicine
        CreateMap<AddMedicineRequestDto, Medicine>()
            .ForMember(dest => dest.MedicineID, opt => opt.MapFrom(_ => Guid.NewGuid()))
            .ForMember(dest => dest.Status,     opt => opt.MapFrom(_ => MedicineStatus.Active))
            .ForMember(dest => dest.CreatedAt,  opt => opt.MapFrom(_ => DateTime.UtcNow))
            .ForMember(dest => dest.Dispenses,  opt => opt.Ignore());

        CreateMap<Medicine, MedicineResponseDto>()
            .ForMember(dest => dest.Type,   opt => opt.MapFrom(src => src.Type.ToString()))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));

        // Dispense
        CreateMap<Dispense, DispenseResponseDto>()
            .ForMember(dest => dest.MedicineName, opt => opt.MapFrom(src => src.Medicine.Name))
            .ForMember(dest => dest.Status,       opt => opt.MapFrom(src => src.Status.ToString()));
    }
}
