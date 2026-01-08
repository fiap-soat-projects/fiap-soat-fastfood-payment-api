using Business.Entities.Enums;
using System.Diagnostics.CodeAnalysis;

namespace Business.Gateways.Clients.DTOs;

[ExcludeFromCodeCoverage]
public record PaymentInput
(
    string CustomerId,
    string CustomerName,
    string CustomerEmail,
    string OrderId,
    decimal TotalPrice,
    PaymentMethod PaymentMethod
)
{ }
