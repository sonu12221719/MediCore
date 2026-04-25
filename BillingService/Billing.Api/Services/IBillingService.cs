using System;
using Billing.Api.DTOs;

namespace Billing.Api.Services;

public interface IBillingService
{
    // Bill
    Task<BillResponseDto> CreateBillAsync(CreateBillRequestDto dto);
    Task<IEnumerable<BillResponseDto>> GetAllBillsAsync();
    Task<BillResponseDto> GetBillByIdAsync(Guid billId);
    Task<IEnumerable<BillResponseDto>> GetBillsByPatientAsync(string patientId);
    Task<BillResponseDto> UpdateBillStatusAsync(Guid billId, UpdateBillStatusRequestDto dto);

    // Payment
    Task<PaymentResponseDto> MakePaymentAsync(MakePaymentRequestDto dto);
    Task<IEnumerable<PaymentResponseDto>> GetPaymentsByBillAsync(Guid billId);
    Task<PaymentResponseDto> GetPaymentByIdAsync(Guid paymentId);

    // Insurance Claim
    Task<InsuranceClaimResponseDto> CreateClaimAsync(CreateInsuranceClaimRequestDto dto);
    Task<InsuranceClaimResponseDto> GetClaimByIdAsync(Guid claimId);
    Task<InsuranceClaimResponseDto> UpdateClaimStatusAsync(Guid claimId, UpdateClaimStatusRequestDto dto);
}
