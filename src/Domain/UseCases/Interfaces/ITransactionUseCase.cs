using Business.Entities;
using Business.Entities.Enums;
using Business.Gateways.Clients.DTOs;

namespace Business.UseCases.Interfaces;
internal interface ITransactionUseCase
{
    Task<PaymentCheckout> CheckoutAsync(CheckoutInput checkoutInput, PaymentMethod method, CancellationToken cancellationToken);
    Task ConfirmPaymentAsync(string orderId, CancellationToken cancellationToken);
    Task ProcessPaymentAsync(string orderId, Payment payment, CancellationToken cancellationToken);
}
