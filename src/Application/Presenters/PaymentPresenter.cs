using Adapter.Presenters.DTOs;
using Business.Entities;
using System.Diagnostics.CodeAnalysis;

namespace Adapter.Presenters;

[ExcludeFromCodeCoverage]
public class PaymentPresenter
{
    public PaymentResponse ViewModel { get; init; }

    public PaymentPresenter(PaymentResult paymentResult)
    {
        ViewModel = new PaymentResponse(
            paymentResult.Id,
            paymentResult.PaymentMethod,
            paymentResult.QrCode,
            paymentResult.QrCodeBase64,
            paymentResult.Amount,
            paymentResult.PaymentStatus);
    }
}
