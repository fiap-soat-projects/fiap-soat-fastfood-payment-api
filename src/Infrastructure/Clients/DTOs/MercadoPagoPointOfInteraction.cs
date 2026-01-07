using System.Text.Json.Serialization;

namespace Infrastructure.Clients.DTOs;

public class MercadoPagoPointOfInteraction
{
    [JsonPropertyName("transaction_data")]
    public MercadoPagoTransactionData? TransactionData { get; init; }
}
