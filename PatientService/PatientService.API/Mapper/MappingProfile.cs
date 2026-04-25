using System;
using AutoMapper;
using PatientLibrary.Entities;
using PatientService.API.DTOs;

namespace PatientService.API.Mapper;

public class MappingProfile:Profile
{
    public MappingProfile()
    {
        CreateMap<RequestPatientDto, Patient>()
            .ForMember(dest=>dest.Status, opt=>opt.MapFrom(src=>true));
        CreateMap<Patient, ResponsePatientDto>();
        CreateMap<UpdatePatientDto,Patient>()
            .ForMember(dest=>dest.PatientID, opt=>opt.Ignore());

        CreateMap<PatientDocument, ResponsePatientDocumentDto>()
            .ForMember(dest=>dest.PatientID, opt=>opt.Ignore());
            
        CreateMap<RequestPatientDocumentDto, PatientDocument>()
            .ForMember(dest=>dest.DocumentID, opt=>opt.MapFrom(src=>Guid.NewGuid()));
    }
}
