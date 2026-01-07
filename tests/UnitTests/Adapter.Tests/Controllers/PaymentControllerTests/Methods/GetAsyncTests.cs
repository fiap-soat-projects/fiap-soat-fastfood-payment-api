using Adapter.Presenters;
using Business.Entities;
using Business.Entities.Enums;
using NSubstitute;

namespace Adapter.Tests.Controllers.PaymentControllerTests.Methods;

public class GetAsyncTests : PaymentControllerTestsBase
{
    [Fact]
    public async Task Have_GetAsync_When_ValidId_Then_Returns_PaymentPresenter()
    {
        // Arrange
        var id = "abc123";

        var returned = new PaymentResult
        {
            Id = id,
            PaymentMethod = PaymentMethod.Pix.ToString(),
            PaymentStatus = PaymentStatus.Pending.ToString(),
            QrCode = "qr123",
            QrCodeBase64 = "base64",
            Amount = 25,
            PaymentResponse = "resp"
        };

        _paymentUseCase.GetAsync(Arg.Any<string>(), Arg.Any<CancellationToken>()).Returns(returned);

        // Act
        var presenter = await _sut.GetAsync(id, CancellationToken.None);

        // Assert
        Assert.NotNull(presenter);
        Assert.IsType<PaymentPresenter>(presenter);
        Assert.Equal(returned.Id, presenter.ViewModel.PaymentId);
        Assert.Equal(returned.Amount, presenter.ViewModel.TotalPrice);
    }
}
