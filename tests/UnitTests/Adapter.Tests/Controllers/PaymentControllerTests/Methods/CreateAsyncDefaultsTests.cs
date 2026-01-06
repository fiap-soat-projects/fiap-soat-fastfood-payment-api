using Adapter.Controllers.DTOs;
using Business.Gateways.Clients.DTOs;
using Business.Entities;
using Business.Entities.Enums;
using NSubstitute;

namespace Adapter.Tests.Controllers.PaymentControllerTests.Methods;

public class CreateAsyncDefaultsTests : PaymentControllerTestsBase
{
    [Fact]
    public async Task Have_CreateAsync_When_NullCustomerFields_Then_Use_Defaults_In_PaymentInput()
    {
        // Arrange
        var request = new PaymentRequest(
            OrderId: "order-1",
            CustomerId: null,
            CustomerName: null,
            CustomerEmail: null,
            TotalPrice: 10,
            PaymentMethod: PaymentMethod.Pix
        );

        var returned = new PaymentResult
        {
            Id = "id-1",
            PaymentMethod = PaymentMethod.Pix.ToString(),
            PaymentStatus = PaymentStatus.Pending.ToString(),
            QrCode = "qr",
            QrCodeBase64 = "b64",
            Amount = 10,
            PaymentResponse = "resp"
        };

        _paymentUseCase.CreateAsync(Arg.Any<PaymentInput>(), Arg.Any<CancellationToken>()).Returns(returned);

        // Act
        var presenter = await _sut.CreateAsync(request, CancellationToken.None);

        // Assert presenter created
        Assert.NotNull(presenter);

        // Assert that use case was called with defaults
        await _paymentUseCase.Received(1).CreateAsync(
            Arg.Is<PaymentInput>(p =>
                p.CustomerId == "NoUser" &&
                p.CustomerName == "fakeUserName" &&
                p.CustomerEmail == "fakeemail@fake.com" &&
                p.OrderId == "order-1" &&
                p.TotalPrice == 10 &&
                p.PaymentMethod == PaymentMethod.Pix
            ),
            Arg.Any<CancellationToken>());
    }
}
