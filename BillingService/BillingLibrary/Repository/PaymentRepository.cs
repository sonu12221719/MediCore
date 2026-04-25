using System;
using BillingLibrary.Data;
using BillingLibrary.Entities;
using BillingLibrary.Enums;
using Microsoft.EntityFrameworkCore;

namespace BillingLibrary.Repository;
public class PaymentRepository : IPaymentRepository
{
    private readonly BillingDbContext _context;

    public PaymentRepository(BillingDbContext context)
    {
        _context = context;
    }

    public async Task<Payment?> GetByIdAsync(Guid paymentId)
    {
        return await _context.Payments
            .Include(p => p.Bill)
            .FirstOrDefaultAsync(p => p.PaymentID == paymentId);
    }

    public async Task<IEnumerable<Payment>> GetByBillIdAsync(Guid billId)
    {
        return await _context.Payments
            .Where(p => p.BillID == billId)
            .OrderByDescending(p => p.Date)
            .ToListAsync();
    }

    // Sum of all completed payments for a bill — used to check remaining amount
    public async Task<decimal> GetTotalPaidAsync(Guid billId)
    {
        return await _context.Payments
            .Where(p => p.BillID == billId && p.Status == PaymentStatus.Completed)
            .SumAsync(p => p.Amount);
    }

    public async Task AddAsync(Payment payment)
    {
        await _context.Payments.AddAsync(payment);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Payment payment)
    {
        payment.UpdatedAt = DateTime.UtcNow;
        _context.Payments.Update(payment);
        await _context.SaveChangesAsync();
    }
}