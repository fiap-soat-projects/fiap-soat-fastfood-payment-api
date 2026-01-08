using Adapter.Controllers.DTOs;
using Adapter.Presenters;
using Business.Entities;
using Business.Entities.Enums;
using Business.Gateways.Clients.DTOs;

namespace Adapter.Tests.StepDefinitions;

[Binding]
public class PaymentControllerStepsDefinition : PaymentControllerTestsBase
{
    private PaymentRequest? _request;
    private PaymentPresenter? _result;

    [Given("a payment request with orderId {string} and totalPrice {int} and paymentMethod {string}")]
    public void GivenAPaymentRequest(string orderId, int totalPrice, string paymentMethod)
    {
        decimal total = Convert.ToDecimal(totalPrice);

        _request = new PaymentRequest(orderId, "cust", "name", "email@e.com", total, Enum.Parse<PaymentMethod>(paymentMethod));
    }

    [When("I call CreateAsync")]
    public async Task WhenICallCreateAsync()
    {
        var paymentResult = new PaymentResult
        {
            Id = "id-1",
            PaymentMethod = PaymentMethod.Pix.ToString(),
            PaymentStatus = PaymentStatus.Pending.ToString(),
            QrCode = "qr",
            QrCodeBase64 = "b64",
            Amount = _request!.TotalPrice,
            PaymentResponse = "resp"
        };

        _paymentUseCase.CreateAsync(Arg.Any<PaymentInput>(), Arg.Any<CancellationToken>()).Returns(paymentResult);

        _result = await _sut.CreateAsync(_request!, CancellationToken.None);
    }

    [Then("the response should contain a payment id")]
    public void ThenResponseShouldContainId()
    {
        Assert.NotNull(_result);
        Assert.False(string.IsNullOrWhiteSpace(_result!.ViewModel.PaymentId));
    }

    [Fact(DisplayName = "BDD: Create payment with valid input (Reqnroll) - scenario runner fallback")]
    public async Task Scenario_CreatePayment_Fallback()
    {
        GivenAPaymentRequest("o-1", 10, "Pix");
        await WhenICallCreateAsync();
        ThenResponseShouldContainId();
    }
}
