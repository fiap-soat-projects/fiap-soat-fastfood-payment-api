using Business.Entities;
using Business.Entities.Enums;
using Business.Gateways.Clients.DTOs;
using NSubstitute;

namespace Business.Tests.UseCases.PaymentUseCase.Methods;

public class CreateAsyncTests : PaymentUseCaseTestsBase
{
    [Fact]
    public async Task Have_CreateAsync_When_ValidInput_Then_Creates_Payment_And_Returns_ResultWithId()
    {
        // Arrange
        var input = new PaymentInput(
            CustomerId: "cust-1",
            CustomerName: "Customer 1",
            CustomerEmail: "cust1@example.com",
            OrderId: "order-1",
            TotalPrice: 15m,
            PaymentMethod: PaymentMethod.Pix
        );

        var mpResult = new PaymentResult
        {
            PaymentMethod = PaymentMethod.Pix.ToString(),
            PaymentStatus = PaymentStatus.Pending.ToString(),
            QrCode = "qr-code",
            QrCodeBase64 = "base64",
            Amount = 15m,
            PaymentResponse = "mp-response"
        };

        _mercadoPagoClient.CreatePaymentAsync(Arg.Any<PaymentInput>(), Arg.Any<CancellationToken>()).Returns(mpResult);
        _paymentRepository.CreateAsync(Arg.Any<Payment>(), Arg.Any<CancellationToken>()).Returns("generated-id");

        // Act
        var result = await _sut.CreateAsync(input, CancellationToken.None);

        // Assert
        await _mercadoPagoClient.Received(1).CreatePaymentAsync(Arg.Is<PaymentInput>(p =>
            p.CustomerId == input.CustomerId &&
            p.CustomerName == input.CustomerName &&
            p.CustomerEmail == input.CustomerEmail &&
            p.OrderId == input.OrderId &&
            p.TotalPrice == input.TotalPrice &&
            p.PaymentMethod == input.PaymentMethod
        ), Arg.Any<CancellationToken>());

        await _paymentRepository.Received(1).CreateAsync(Arg.Is<Payment>(p =>
            p.OrderId == input.OrderId &&
            p.CustomerId == input.CustomerId &&
            p.CustomerName == input.CustomerName &&
            p.CustomerEmail == input.CustomerEmail &&
            p.TotalPrice == input.TotalPrice &&
            p.QrCode == mpResult.QrCode &&
            p.QrCodeBase64 == mpResult.QrCodeBase64 &&
            p.PaymentMethod == input.PaymentMethod &&
            p.PaymentStatus == PaymentStatus.Pending &&
            p.PaymentResponse == mpResult.PaymentResponse
        ), Arg.Any<CancellationToken>());

        Assert.Equal("generated-id", result.Id);
    }

    [Fact]
    public async Task CreateAsync_Sets_Id_On_Returned_PaymentResult_Instance()
    {
        // Arrange
        var input = new PaymentInput(
            CustomerId: "cust-2",
            CustomerName: "Customer 2",
            CustomerEmail: "cust2@example.com",
            OrderId: "order-2",
            TotalPrice: 20m,
            PaymentMethod: PaymentMethod.Pix
        );

        var mpResult = new PaymentResult
        {
            PaymentMethod = PaymentMethod.Pix.ToString(),
            PaymentStatus = PaymentStatus.Pending.ToString(),
            QrCode = "qr-2",
            QrCodeBase64 = "base64-2",
            Amount = 20m,
            PaymentResponse = "mp-resp-2"
        };

        _mercadoPagoClient.CreatePaymentAsync(Arg.Any<PaymentInput>(), Arg.Any<CancellationToken>()).Returns(mpResult);
        _paymentRepository.CreateAsync(Arg.Any<Payment>(), Arg.Any<CancellationToken>()).Returns("id-123");

        // Act
        var result = await _sut.CreateAsync(input, CancellationToken.None);

        // Assert: same instance returned and id assigned
        Assert.Same(mpResult, result);
        Assert.Equal("id-123", result.Id);
    }
}
