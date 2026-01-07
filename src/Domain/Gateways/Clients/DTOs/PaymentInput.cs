using Business.Entities.Enums;

namespace Business.Gateways.Clients.DTOs;
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
