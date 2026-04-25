using Billing.Api.DTOs;
using Billing.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Billing.Api.Controllers
{
    [ApiController]
    [Route("api/billing")]
    [Authorize]
    public class BillingController : ControllerBase
    {
        private readonly IBillingService _billingService;

        public BillingController(IBillingService billingService)
        {
            _billingService = billingService;
        }

        // ── BILL ──────────────────────────────────

        // POST /api/billing/bills
        [HttpPost("bills")]
        [Authorize(Roles = "Finance,Admin")]
        public async Task<IActionResult> CreateBill([FromBody] CreateBillRequestDto dto)
        {
            var result = await _billingService.CreateBillAsync(dto);
            return CreatedAtAction(nameof(GetBillById), new { id = result.BillID }, result);
        }

        // GET /api/billing/bills
        [HttpGet("bills")]
        [Authorize(Roles = "Finance,Admin")]
        public async Task<IActionResult> GetAllBills()
        {
            var result = await _billingService.GetAllBillsAsync();
            return Ok(result);
        }

        // GET /api/billing/bills/{id}
        [HttpGet("bills/{id:guid}")]
        [Authorize(Roles = "Finance,Admin,Patient,Doctor")]
        public async Task<IActionResult> GetBillById(Guid id)
        {
            var result = await _billingService.GetBillByIdAsync(id);
            return Ok(result);
        }

        // GET /api/billing/bills/patient/{patientId}
        [HttpGet("bills/patient/{patientId}")]
        [Authorize(Roles = "Finance,Admin,Doctor")]
        public async Task<IActionResult> GetBillsByPatient(string patientId)
        {
            var result = await _billingService.GetBillsByPatientAsync(patientId);
            return Ok(result);
        }

        // PUT /api/billing/bills/{id}/status
        [HttpPut("bills/{id:guid}/status")]
        [Authorize(Roles = "Finance,Admin")]
        public async Task<IActionResult> UpdateBillStatus(Guid id, [FromBody] UpdateBillStatusRequestDto dto)
        {
            var result = await _billingService.UpdateBillStatusAsync(id, dto);
            return Ok(result);
        }


        // ── PAYMENT ───────────────────────────────

        // POST /api/billing/payments
        [HttpPost("payments")]
        [Authorize(Roles = "Patient,Finance")]
        public async Task<IActionResult> MakePayment([FromBody] MakePaymentRequestDto dto)
        {
            var result = await _billingService.MakePaymentAsync(dto);
            return CreatedAtAction(nameof(GetPaymentById), new { id = result.PaymentID }, result);
        }

        // GET /api/billing/payments/{id}
        [HttpGet("payments/{id:guid}")]
        [Authorize(Roles = "Finance,Admin,Patient")]
        public async Task<IActionResult> GetPaymentById(Guid id)
        {
            var result = await _billingService.GetPaymentByIdAsync(id);
            return Ok(result);
        }

        // GET /api/billing/payments/bill/{billId}
        [HttpGet("payments/bill/{billId:guid}")]
        [Authorize(Roles = "Finance,Admin,Patient,Doctor")]
        public async Task<IActionResult> GetPaymentsByBill(Guid billId)
        {
            var result = await _billingService.GetPaymentsByBillAsync(billId);
            return Ok(result);
        }


        // ── INSURANCE CLAIM ───────────────────────

        // POST /api/billing/claims
        [HttpPost("claims")]
        [Authorize(Roles = "Patient,Finance")]
        public async Task<IActionResult> CreateClaim([FromBody] CreateInsuranceClaimRequestDto dto)
        {
            var result = await _billingService.CreateClaimAsync(dto);
            return CreatedAtAction(nameof(GetClaimById), new { id = result.ClaimID }, result);
        }

        // GET /api/billing/claims/{id}
        [HttpGet("claims/{id:guid}")]
        [Authorize(Roles = "Finance,Admin,Patient")]
        public async Task<IActionResult> GetClaimById(Guid id)
        {
            var result = await _billingService.GetClaimByIdAsync(id);
            return Ok(result);
        }

        // PUT /api/billing/claims/{id}/status
        [HttpPut("claims/{id:guid}/status")]
        [Authorize(Roles = "Finance,Admin")]
        public async Task<IActionResult> UpdateClaimStatus(Guid id, [FromBody] UpdateClaimStatusRequestDto dto)
        {
            var result = await _billingService.UpdateClaimStatusAsync(id, dto);
            return Ok(result);
        }
    }
}
