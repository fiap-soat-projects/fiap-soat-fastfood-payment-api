using Business.Entities.Enums;
using System.Diagnostics.CodeAnalysis;

namespace Adapter.Controllers.DTOs;

[ExcludeFromCodeCoverage]
public record PaymentRequest
(
    string? OrderId,
    string? CustomerId,
    string? CustomerName,
    string? CustomerEmail, 
    decimal TotalPrice,
    PaymentMethod PaymentMethod)
{}


