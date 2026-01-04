using System.Globalization;

namespace Adapter.Controllers.DTOs;

public record CheckoutRequest
{
    private string? _paymentType;

    public string? PaymentType
    {
        get => _paymentType;
        init
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(value);

            var paymentType = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(value.ToLower());

            _paymentType = paymentType;
        }
    }

    public required string OrderId { get; set; }
    public string? CustomerId { get; set; }
    public string? CustomerName { get; set; }
    public string? CustomerEmail { get; set; }
    public required decimal TotalPrice { get; set; }

}
