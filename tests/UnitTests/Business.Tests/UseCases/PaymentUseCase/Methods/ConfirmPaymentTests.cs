using Business.Entities.Enums;
using NSubstitute;

namespace Business.Tests.UseCases.PaymentUseCase.Methods;

public class ConfirmPaymentTests : PaymentUseCaseTestsBase
{
    [Fact]
    public async Task Have_ConfirmPaymentAsync_When_ValidId_Then_Calls_UpdateStatus()
    {
        // Arrange
        var id = "to-confirm";

        // Act
        await _sut.ConfirmPaymentAsync(id, CancellationToken.None);

        // Assert
        await _paymentRepository.Received(1).UpdateStatusAsync(id, PaymentStatus.Authorized, Arg.Any<CancellationToken>());
    }
}
