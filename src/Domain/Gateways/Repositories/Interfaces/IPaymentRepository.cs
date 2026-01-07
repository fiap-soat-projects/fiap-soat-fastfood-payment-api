using Business.Entities;
using Business.Entities.Enums;

namespace Business.Gateways.Repositories.Interfaces;

public interface IPaymentRepository
{
    Task<string> CreateAsync(Payment payment, CancellationToken cancellationToken);
    Task<Payment?> GetByIdAsync(string id, CancellationToken cancellationToken);    
    Task UpdateStatusAsync(string id, PaymentStatus paymentStatus, CancellationToken cancellationToken);
}
