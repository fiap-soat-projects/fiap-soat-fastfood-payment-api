using System.Text.Json.Serialization;

namespace Infrastructure.Clients.DTOs;

public class MercadoPagoTransactionData
{
    [JsonPropertyName("qr_code")]
    public string? QrCode { get; init; }

    [JsonPropertyName("qr_code_base64")]
    public string? QrCodeBase64 { get; init; }
}
