using Business.Entities;
using Business.Entities.Enums;
using Business.Gateways.Clients.DTOs;
using Business.Gateways.Clients.Interfaces;
using Business.Gateways.Repositories.Interfaces;
using Business.UseCases.Interfaces;

namespace Business.UseCases;

public class PaymentUseCase : IPaymentUseCase
{
    private readonly IMercadoPagoClient _mercadoPagoClient;
    private readonly IPaymentRepository _paymentRepository;

    public PaymentUseCase(IMercadoPagoClient mercadoPagoClient,
        IPaymentRepository paymentRepository)
    {
        _mercadoPagoClient = mercadoPagoClient;
        _paymentRepository = paymentRepository;
    }

    public async Task<PaymentResult> CreateAsync(PaymentInput checkoutInput, CancellationToken cancellationToken)
    {
        var paymentResult = await CreatePaymentAsync(checkoutInput, cancellationToken);

        var payment = new Payment(
            checkoutInput.OrderId,
            checkoutInput.CustomerId,
            checkoutInput.CustomerName,
            checkoutInput.CustomerEmail,
            checkoutInput.TotalPrice,
            paymentResult.QrCode,
            paymentResult.QrCodeBase64,
            checkoutInput.PaymentMethod,
            PaymentStatus.Pending,
            paymentResult.PaymentResponse);

        var paymentId = await _paymentRepository.CreateAsync(payment, cancellationToken);
        paymentResult.Id = paymentId;

        return paymentResult;
    }

    public async Task<PaymentResult> GetAsync(string id, CancellationToken cancellationToken)
    {
        var payment = await _paymentRepository.GetByIdAsync(id, cancellationToken);

        return new PaymentResult
        {
            Id = payment.Id!,
            PaymentMethod = payment.PaymentMethod.ToString(),
            PaymentStatus = payment.PaymentStatus.ToString(),
            Amount = payment.TotalPrice!,
            PaymentResponse = payment.PaymentResponse!,
            QrCode = payment.QrCode!,
            QrCodeBase64 = payment.QrCodeBase64!
        };

    }

    private async Task<PaymentResult> CreatePaymentAsync(PaymentInput checkoutInput, CancellationToken cancellationToken)
    {
        return await _mercadoPagoClient.CreatePaymentAsync(checkoutInput!, cancellationToken);
    }

    public async Task ConfirmPaymentAsync(string id, CancellationToken cancellationToken)
    {
        await _paymentRepository.UpdateStatusAsync(id, PaymentStatus.Authorized, cancellationToken);
    }
}
