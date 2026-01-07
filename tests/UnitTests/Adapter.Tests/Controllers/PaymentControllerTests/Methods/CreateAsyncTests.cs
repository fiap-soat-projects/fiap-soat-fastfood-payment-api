using Adapter.Controllers.DTOs;
using Adapter.Presenters;
using Business.Entities;
using Business.Entities.Enums;
using Business.Gateways.Clients.DTOs;
using NSubstitute;

namespace Adapter.Tests.Controllers.PaymentControllerTests.Methods;

public class CreateAsyncTests : PaymentControllerTestsBase
{
    [Fact]
    public async Task Have_CreateAsync_When_ValidInput_Then_Returns_PaymentPresenter()
    {
        #region Arrange
        var request = new PaymentRequest(
            OrderId: "1234",
            CustomerId: "234234",
            CustomerName: "teste",
            CustomerEmail: "teste@teste.com",
            TotalPrice: 19,
            PaymentMethod: PaymentMethod.Pix
            );     

        var returned = new PaymentResult
        {
            Id = "123",
            PaymentMethod = PaymentMethod.Pix.ToString(),
            PaymentStatus = PaymentStatus.Pending.ToString(),
            QrCode = "1231231231312",
            QrCodeBase64 = "1231231231312",
            Amount = 19,
            PaymentResponse = "897123987139812"           
        };


        _paymentUseCase.CreateAsync(Arg.Any<PaymentInput>(), Arg.Any<CancellationToken>()).Returns(returned);
        #endregion

        // Act
        var presenter = await _sut.CreateAsync(request, CancellationToken.None);

        // Assert
        Assert.NotNull(presenter);
        Assert.IsType<PaymentPresenter>(presenter);        
        Assert.Equal(returned.Id, presenter.ViewModel.PaymentId);
        Assert.Equal(returned.Amount, presenter.ViewModel.TotalPrice);
    }
}
