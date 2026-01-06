using Business.Entities.Enums;

namespace Adapter.Controllers.DTOs;

public record PaymentRequest
(
    string? OrderId,
    string? CustomerId,
    string? CustomerName,
    string? CustomerEmail, 
    decimal TotalPrice,
    PaymentMethod PaymentMethod)
{}


