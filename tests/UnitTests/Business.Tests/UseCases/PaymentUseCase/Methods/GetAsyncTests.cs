using Business.Entities;
using Business.Entities.Enums;
using NSubstitute;
using System.Reflection;

namespace Business.Tests.UseCases.PaymentUseCase.Methods;

public class GetAsyncTests : PaymentUseCaseTestsBase
{
    [Fact]
    public async Task Have_GetAsync_When_ValidId_Then_Returns_Mapped_PaymentResult()
    {
        // Arrange
        var id = "pmt-1";

        var paymentArgs = new object?[]
        {
            id,
            "order-1",
            "cust-1",
            "Customer 1",
            "cust1@example.com",
            12m,
            "qr-1",
            "b64-1",
            PaymentMethod.Pix,
            PaymentStatus.Pending,
            "resp-1"
        };

        var payment = (Payment)Activator.CreateInstance(typeof(Payment), BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public, null, paymentArgs, null)!;

        _paymentRepository.GetByIdAsync(Arg.Is(id), Arg.Any<CancellationToken>()).Returns(payment);

        // Act
        var result = await _sut.GetAsync(id, CancellationToken.None);

        // Assert mapping
        Assert.Equal(payment.Id, result.Id);
        Assert.Equal(payment.PaymentMethod.ToString(), result.PaymentMethod);
        Assert.Equal(payment.PaymentStatus.ToString(), result.PaymentStatus);
        Assert.Equal(payment.TotalPrice, result.Amount);
        Assert.Equal(payment.PaymentResponse, result.PaymentResponse);
        Assert.Equal(payment.QrCode, result.QrCode);
        Assert.Equal(payment.QrCodeBase64, result.QrCodeBase64);

        await _paymentRepository.Received(1).GetByIdAsync(id, Arg.Any<CancellationToken>());
    }
}
