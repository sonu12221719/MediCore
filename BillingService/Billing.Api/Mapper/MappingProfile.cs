using System;
using AutoMapper;
using Billing.Api.DTOs;
using BillingLibrary.Entities;
using BillingLibrary.Enums;

namespace Billing.Api.Mapper;

public class MappingProfile:Profile
{
    public MappingProfile()
    {
        // Bill
        CreateMap<CreateBillRequestDto, Bill>()
            .ForMember(dest => dest.BillID,     opt => opt.MapFrom(_ => Guid.NewGuid()))
            .ForMember(dest => dest.Status,     opt => opt.MapFrom(_ => BillStatus.Draft))
            .ForMember(dest => dest.PaidAmount, opt => opt.MapFrom(_ => 0m))
            .ForMember(dest => dest.Date,       opt => opt.MapFrom(_ => DateTime.UtcNow))
            .ForMember(dest => dest.Payments,        opt => opt.Ignore())
            .ForMember(dest => dest.InsuranceClaims, opt => opt.Ignore());

        CreateMap<Bill, BillResponseDto>()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));

        // Payment
        CreateMap<MakePaymentRequestDto, Payment>()
            .ForMember(dest => dest.PaymentID, opt => opt.MapFrom(_ => Guid.NewGuid()))
            .ForMember(dest => dest.Status,    opt => opt.MapFrom(_ => PaymentStatus.Completed))
            .ForMember(dest => dest.Date,      opt => opt.MapFrom(_ => DateTime.UtcNow))
            .ForMember(dest => dest.Bill,      opt => opt.Ignore());

        CreateMap<Payment, PaymentResponseDto>()
            .ForMember(dest => dest.Method, opt => opt.MapFrom(src => src.Method.ToString()))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));

        // InsuranceClaim
        CreateMap<CreateInsuranceClaimRequestDto, InsuranceClaim>()
            .ForMember(dest => dest.ClaimID,   opt => opt.MapFrom(_ => Guid.NewGuid()))
            .ForMember(dest => dest.Status,    opt => opt.MapFrom(_ => ClaimStatus.Submitted))
            .ForMember(dest => dest.Date,      opt => opt.MapFrom(_ => DateTime.UtcNow))
            .ForMember(dest => dest.PatientID, opt => opt.Ignore())   // set from token
            .ForMember(dest => dest.Bill,      opt => opt.Ignore());

        CreateMap<InsuranceClaim, InsuranceClaimResponseDto>()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()));
    
    }
}
