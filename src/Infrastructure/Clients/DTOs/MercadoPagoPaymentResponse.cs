using Business.Entities;
using Business.Entities.Enums;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Infrastructure.Clients.DTOs;

public class MercadoPagoPaymentResponse
{
    [JsonPropertyName("id")]
    public long Id { get; init; }

    [JsonPropertyName("payment_method_id")]
    public string? PaymentMethodId { get; init; }

    [JsonPropertyName("transaction_amount")]
    public decimal TransactionAmount { get; init; }

    [JsonPropertyName("currency_id")]
    public string? CurrencyId { get; init; }

    [JsonPropertyName("metadata")]
    public MercadoPagoMetadata? Metadata { get; init; }

    [JsonPropertyName("point_of_interaction")]
    public MercadoPagoPointOfInteraction? PointOfInteraction { get; init; }

    internal PaymentResult ToDomain()
    {
        var orderPaymentCheckout = new PaymentResult
        {            
            PaymentMethod = PaymentMethodId!,
            PaymentStatus = PaymentStatus.Pending.ToString(),
            QrCode = PointOfInteraction?.TransactionData?.QrCode!,
            QrCodeBase64 = PointOfInteraction?.TransactionData?.QrCodeBase64!,
            Amount = TransactionAmount,
            PaymentResponse = JsonSerializer.Serialize(this)
        };

        return orderPaymentCheckout;
    }
}
