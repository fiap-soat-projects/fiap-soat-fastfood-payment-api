using Business.Entities.Enums;
using System.Diagnostics.CodeAnalysis;

namespace Adapter.Presenters.DTOs;

[ExcludeFromCodeCoverage]
public record PaymentResponse(string PaymentId, string PaymentMethod, string QrCode, string QrCodeBase64, decimal TotalPrice, string PaymentStatus) { }
