using Business.Entities;
using Business.Gateways.Clients.DTOs;

namespace Business.UseCases.Interfaces;

internal interface IPaymentUseCase
{
    Task<PaymentResult> CreateAsync(PaymentInput checkoutInput, CancellationToken cancellationToken);
    Task<PaymentResult> GetAsync(string id, CancellationToken cancellationToken);
    Task ConfirmPaymentAsync(string id, CancellationToken cancellationToken);
}
