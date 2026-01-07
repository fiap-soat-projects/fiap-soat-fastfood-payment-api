using Adapter.Presenters.DTOs;
using Business.Entities;

namespace Adapter.Presenters;
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
