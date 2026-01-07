using Adapter.Controllers.DTOs;
using Adapter.Controllers.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Endpoints;

[ApiController]
[Route("/v1/payment")]
public class Payment : ControllerBase
{
    private readonly IPaymentController _paymentController;

    public Payment(IPaymentController paymentController)
    {
        _paymentController = paymentController;
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateAsync(
        [FromBody] PaymentRequest paymentRequest,
        CancellationToken cancellationToken)
    {
        var presenter = await _paymentController.CreateAsync(paymentRequest, cancellationToken);
        return Ok(presenter.ViewModel);
    }

    [HttpGet("{id:length(24)}")]
    public async Task<IActionResult> GetAsync(string id, CancellationToken cancellationToken)
    {
        var presenter = await _paymentController.GetAsync(id, cancellationToken);
        return Ok(presenter.ViewModel);
    }

    [HttpPost("{id:length(24)}/confirm-payment")]
    public async Task<IActionResult> ConfirmAsync(string id, CancellationToken cancellationToken)
    {
        await _paymentController.ConfirmAsync(id, cancellationToken);
        return NoContent();
    }


}
