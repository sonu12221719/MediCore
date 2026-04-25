using System;
using BillingLibrary.Entities;

namespace BillingLibrary.Repository;

public interface IPaymentRepository
{
    Task<Payment?> GetByIdAsync(Guid paymentId);
    Task<IEnumerable<Payment>> GetByBillIdAsync(Guid billId);
    Task<decimal> GetTotalPaidAsync(Guid billId);              // sum of all completed payments
    Task AddAsync(Payment payment);
    Task UpdateAsync(Payment payment);
}
