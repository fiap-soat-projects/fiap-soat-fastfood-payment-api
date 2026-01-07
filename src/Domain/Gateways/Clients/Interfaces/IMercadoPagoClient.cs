using Business.Entities;
using Business.Entities.Enums;
using Business.Gateways.Clients.DTOs;

namespace Business.Gateways.Clients.Interfaces;

public interface IMercadoPagoClient
{
    Task<PaymentResult> CreatePaymentAsync(
        PaymentInput paymentInput,
        CancellationToken cancellationToken);
}
