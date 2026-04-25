using System;
using System.Security.Claims;
using AutoMapper;
using Billing.Api.DTOs;
using BillingLibrary.Entities;
using BillingLibrary.Enums;
using BillingLibrary.Exceptions;
using BillingLibrary.Repository;

namespace Billing.Api.Services;

public class BillingService:IBillingService
{
    private readonly IBillRepository _billRepository;
    private readonly IPaymentRepository _paymentRepository;
    private readonly IInsuranceClaimRepository _claimRepository;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public BillingService(
        IBillRepository billRepository,
        IPaymentRepository paymentRepository,
        IInsuranceClaimRepository claimRepository,
        IMapper mapper,
        IHttpContextAccessor httpContextAccessor)
    {
        _billRepository          = billRepository;
        _paymentRepository       = paymentRepository;
        _claimRepository         = claimRepository;
        _mapper                  = mapper;
        _httpContextAccessor     = httpContextAccessor;
    }

    private string GetUserIdFromToken()
        => _httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.NameIdentifier)!;


    // ── BILL ──────────────────────────────────

    public async Task<BillResponseDto> CreateBillAsync(CreateBillRequestDto dto)
    {
        if (dto.Amount <= 0)
            throw new BillingServiceException("Bill amount must be greater than zero.");

        var bill = _mapper.Map<Bill>(dto);
        await _billRepository.AddAsync(bill);
        return _mapper.Map<BillResponseDto>(bill);
    }

    public async Task<IEnumerable<BillResponseDto>> GetAllBillsAsync()
    {
        var bills = await _billRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<BillResponseDto>>(bills);
    }

    public async Task<BillResponseDto> GetBillByIdAsync(Guid billId)
    {
        var bill = await _billRepository.GetByIdAsync(billId)
            ?? throw new BillNotFoundException(billId);

        return _mapper.Map<BillResponseDto>(bill);
    }

    public async Task<IEnumerable<BillResponseDto>> GetBillsByPatientAsync(string patientId)
    {
        var bills = await _billRepository.GetByPatientIdAsync(patientId);
        return _mapper.Map<IEnumerable<BillResponseDto>>(bills);
    }

    public async Task<BillResponseDto> UpdateBillStatusAsync(Guid billId, UpdateBillStatusRequestDto dto)
    {
        var bill = await _billRepository.GetByIdAsync(billId)
            ?? throw new BillNotFoundException(billId);

        bill.Status = dto.Status;
        await _billRepository.UpdateAsync(bill);
        return _mapper.Map<BillResponseDto>(bill);
    }


    // ── PAYMENT ───────────────────────────────

    public async Task<PaymentResponseDto> MakePaymentAsync(MakePaymentRequestDto dto)
    {
        // 1. Get bill
        var bill = await _billRepository.GetByIdAsync(dto.BillID)
            ?? throw new BillNotFoundException(dto.BillID);

        // 2. Guard checks
        if (bill.Status == BillStatus.Paid)
            throw new BillAlreadyPaidException(dto.BillID);

        if (bill.Status == BillStatus.Cancelled)
            throw new BillCancelledException(dto.BillID);

        if (dto.Amount <= 0)
            throw new BillingServiceException("Payment amount must be greater than zero.");

        // 3. Check overpayment
        var remaining = bill.Amount - bill.PaidAmount;
        if (dto.Amount > remaining)
            throw new OverpaymentException(remaining);

        // 4. Create payment record
        var payment = _mapper.Map<Payment>(dto);
        await _paymentRepository.AddAsync(payment);

        // 5. Update bill paid amount
        bill.PaidAmount += dto.Amount;

        // 6. Auto-update bill status
        bill.Status = bill.PaidAmount >= bill.Amount
            ? BillStatus.Paid
            : BillStatus.PartiallyPaid;

        await _billRepository.UpdateAsync(bill);

        return _mapper.Map<PaymentResponseDto>(payment);
    }

    public async Task<IEnumerable<PaymentResponseDto>> GetPaymentsByBillAsync(Guid billId)
    {
        _ = await _billRepository.GetByIdAsync(billId)
            ?? throw new BillNotFoundException(billId);

        var payments = await _paymentRepository.GetByBillIdAsync(billId);
        return _mapper.Map<IEnumerable<PaymentResponseDto>>(payments);
    }

    public async Task<PaymentResponseDto> GetPaymentByIdAsync(Guid paymentId)
    {
        var payment = await _paymentRepository.GetByIdAsync(paymentId)
            ?? throw new PaymentNotFoundException(paymentId);

        return _mapper.Map<PaymentResponseDto>(payment);
    }


    // ── INSURANCE CLAIM ───────────────────────

    public async Task<InsuranceClaimResponseDto> CreateClaimAsync(CreateInsuranceClaimRequestDto dto)
    {
        // 1. Get bill
        var bill = await _billRepository.GetByIdAsync(dto.BillID)
            ?? throw new BillNotFoundException(dto.BillID);

        // 2. Guard checks
        if (bill.Status == BillStatus.Paid)
            throw new BillingServiceException("Cannot submit claim for a fully paid bill.");

        if (bill.Status == BillStatus.Cancelled)
            throw new BillCancelledException(dto.BillID);

        if (dto.Amount <= 0 || dto.Amount > bill.Amount)
            throw new BillingServiceException($"Claim amount must be between 1 and {bill.Amount:C}.");

        // 3. Duplicate active claim check
        var existing = await _claimRepository.GetActiveclaimByBillIdAsync(dto.BillID);
        if (existing is not null)
            throw new DuplicateClaimException(dto.BillID);

        // 4. Create claim
        var claim = _mapper.Map<InsuranceClaim>(dto);
        claim.PatientID = GetUserIdFromToken();

        await _claimRepository.AddAsync(claim);
        return _mapper.Map<InsuranceClaimResponseDto>(claim);
    }

    public async Task<InsuranceClaimResponseDto> GetClaimByIdAsync(Guid claimId)
    {
        var claim = await _claimRepository.GetByIdAsync(claimId)
            ?? throw new ClaimNotFoundException(claimId);

        return _mapper.Map<InsuranceClaimResponseDto>(claim);
    }

    public async Task<InsuranceClaimResponseDto> UpdateClaimStatusAsync(Guid claimId, UpdateClaimStatusRequestDto dto)
    {
        var claim = await _claimRepository.GetByIdAsync(claimId)
            ?? throw new ClaimNotFoundException(claimId);

        claim.Status = dto.Status;
        claim.Notes  = dto.Notes ?? claim.Notes;

        // If claim settled — apply claim amount to bill PaidAmount
        if (dto.Status == ClaimStatus.Settled)
        {
            var bill = await _billRepository.GetByIdAsync(claim.BillID)
                ?? throw new BillNotFoundException(claim.BillID);

            bill.PaidAmount += claim.Amount;
            bill.Status = bill.PaidAmount >= bill.Amount
                ? BillStatus.Paid
                : BillStatus.PartiallyPaid;

            await _billRepository.UpdateAsync(bill);
        }

        await _claimRepository.UpdateAsync(claim);
        return _mapper.Map<InsuranceClaimResponseDto>(claim);
    }
}
