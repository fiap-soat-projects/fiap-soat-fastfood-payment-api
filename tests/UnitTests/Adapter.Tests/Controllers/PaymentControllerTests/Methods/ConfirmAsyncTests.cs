using NSubstitute;

namespace Adapter.Tests.Controllers.PaymentControllerTests.Methods;

public class ConfirmAsyncTests : PaymentControllerTestsBase
{
    [Fact]
    public async Task Have_ConfirmAsync_When_ValidId_Then_Calls_UseCase()
    {
        // Arrange
        var id = "confirmId";

        // Act
        await _sut.ConfirmAsync(id, CancellationToken.None);

        // Assert
        await _paymentUseCase.Received(1).ConfirmPaymentAsync(id, Arg.Any<CancellationToken>());
    }
}
