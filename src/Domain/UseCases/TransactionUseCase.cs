using Business.Entities;
using Business.Entities.Enums;
using Business.Gateways.Clients.DTOs;
using Business.Gateways.Clients.Interfaces;
using Business.Gateways.Repositories.Interfaces;
using Business.UseCases.Interfaces;

namespace Business.UseCases;

internal class TransactionUseCase : ITransactionUseCase
{
    private readonly IPixClient _pixClient;
    private readonly IOrderRepository _orderRepository;

    public TransactionUseCase(IPixClient pixClient,
        IOrderRepository orderRepository)
    {
        _pixClient = pixClient;
        _orderRepository = orderRepository;
    }

    public async Task<PaymentCheckout> CheckoutAsync(CheckoutInput checkoutInput, PaymentMethod method, CancellationToken cancellationToken)
    {
        var paymentCheckout = await ExecuteCheckoutAsync(checkoutInput, method, cancellationToken);

        var payment = new Payment
        {
            Id = paymentCheckout.Id.ToString(),
            Method = method,
            Status = PaymentStatus.Pending
        };

        await _orderRepository.UpdatePaymentAsync(checkoutInput.OrderId, OrderStatus.Pending, payment, cancellationToken);

        return paymentCheckout;
    }

    private async Task<PaymentCheckout> ExecuteCheckoutAsync(CheckoutInput checkoutInput, PaymentMethod method, CancellationToken cancellationToken)
    {
        return await ExecuteOrderCheckoutAsync(checkoutInput!, method, cancellationToken);
    }

    public async Task ConfirmPaymentAsync(string id, CancellationToken cancellationToken)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(id, nameof(id));

        await _orderRepository.UpdateStatusAsync(id, OrderStatus.Received, cancellationToken);
    }

    public async Task ProcessPaymentAsync(string orderId, Payment payment, CancellationToken cancellationToken)
    {
        var orderStatus = GetOrderStatusByPayment(payment.Status);

        await _orderRepository.UpdatePaymentAsync(orderId, orderStatus, payment, cancellationToken);
    }
    private static OrderStatus GetOrderStatusByPayment(PaymentStatus payment)
    {
        return payment switch
        {
            PaymentStatus.Pending => OrderStatus.Pending,
            PaymentStatus.Authorized => OrderStatus.Received,
            PaymentStatus.Refused => OrderStatus.Canceled,
            _ => OrderStatus.None
        };

    }

    private async Task<PaymentCheckout> ExecuteOrderCheckoutAsync(CheckoutInput checkoutInput, PaymentMethod paymentMethod, CancellationToken cancellationToken)
    {
        var orderPaymentCheckout = await _pixClient.CreatePaymentAsync(checkoutInput!, paymentMethod, cancellationToken);
        return orderPaymentCheckout;
    }
}
