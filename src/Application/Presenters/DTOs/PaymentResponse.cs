using Business.Entities.Enums;

namespace Adapter.Presenters.DTOs;

public record PaymentResponse(string PaymentId, string PaymentMethod, string QrCode, string QrCodeBase64, decimal TotalPrice, string PaymentStatus) { }
