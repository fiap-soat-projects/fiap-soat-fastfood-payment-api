using Adapter.Presenters.DTOs;
using Business.Entities;
using Business.Entities.Enums;

namespace Adapter.Tests.Controllers.PaymentControllerTests.Methods;

public class PaymentPresenterTests
{
    [Fact]
    public void Have_PaymentPresenter_Map_Properly_From_PaymentResult()
    {
        var paymentResult = new PaymentResult
        {
            Id = "id-pp",
            PaymentMethod = PaymentMethod.Pix.ToString(),
            PaymentStatus = PaymentStatus.Pending.ToString(),
            QrCode = "qr",
            QrCodeBase64 = "b64",
            Amount = 5,
            PaymentResponse = "resp"
        };

        var presenter = new Adapter.Presenters.PaymentPresenter(paymentResult);

        Assert.NotNull(presenter.ViewModel);
        Assert.IsType<PaymentResponse>(presenter.ViewModel);
        Assert.Equal(paymentResult.Id, presenter.ViewModel.PaymentId);
        Assert.Equal(paymentResult.PaymentMethod, presenter.ViewModel.PaymentMethod);
        Assert.Equal(paymentResult.QrCode, presenter.ViewModel.QrCode);
        Assert.Equal(paymentResult.QrCodeBase64, presenter.ViewModel.QrCodeBase64);
        Assert.Equal(paymentResult.Amount, presenter.ViewModel.TotalPrice);
        Assert.Equal(paymentResult.PaymentStatus, presenter.ViewModel.PaymentStatus);
    }
}
