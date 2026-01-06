using Business.Entities.Enums;

namespace Adapter.Controllers.DTOs;

public record PaymentRequest
{
    public required string OrderId { get; set; }
    public string? CustomerId { get; set; }
    public string? CustomerName { get; set; }
    public string? CustomerEmail { get; set; }
    public required decimal TotalPrice { get; set; }
    public required PaymentMethod PaymentMethod { get; set; }

}
