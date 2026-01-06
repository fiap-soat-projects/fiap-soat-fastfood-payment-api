using Adapter.Controllers.DTOs;
using Adapter.Controllers.Interfaces;
using Adapter.Presenters;
using Business.Gateways.Clients.DTOs;
using Business.UseCases.Interfaces;

namespace Adapter.Controllers;

public class PaymentController : IPaymentController
{
    private readonly IPaymentUseCase _paymentUseCase;

    public PaymentController(
        IPaymentUseCase paymentUseCase)
    {
        _paymentUseCase = paymentUseCase;
    }

    public async Task<PaymentPresenter> CreateAsync(PaymentRequest request, CancellationToken cancellationToken)
    {

        var customerId = !string.IsNullOrWhiteSpace(request.CustomerId) ? request.CustomerId : "NoUser";
        var customerName = !string.IsNullOrWhiteSpace(request.CustomerName) ? request.CustomerName : "fakeUserName";
        var customerEmail = !string.IsNullOrWhiteSpace(request.CustomerEmail) ? request.CustomerEmail : "fakeemail@fake.com";

        var paymentInput = new PaymentInput
        (
            customerId!,
            customerName!,
            customerEmail!,
            request.OrderId!,
            request.TotalPrice,
            request.PaymentMethod
        );

        var paymentResult = await _paymentUseCase.CreateAsync(paymentInput, cancellationToken);
        return new PaymentPresenter(paymentResult);
    }

    public async Task<PaymentPresenter> GetAsync(string id, CancellationToken cancellationToken)
    {
        var paymentResult = await _paymentUseCase.GetAsync(id, cancellationToken);
        return new PaymentPresenter(paymentResult);
    }

    public async Task ConfirmAsync(string id, CancellationToken cancellationToken)
    {
        await _paymentUseCase.ConfirmPaymentAsync(id, cancellationToken);
    }    

}
