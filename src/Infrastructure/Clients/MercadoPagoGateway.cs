using Business.Entities;
using Business.Gateways.Clients.DTOs;
using Business.Gateways.Clients.Interfaces;
using Infrastructure.Clients.DTOs;
using Microsoft.Extensions.Logging;
using System.Runtime.InteropServices.Marshalling;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Infrastructure.Clients;

public class MercadoPagoGateway : IMercadoPagoClient
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<MercadoPagoGateway> _logger; 
    private const string BASE_URL_MOCK = "https://webhook.site/942f0c97-2b54-4896-830f-7e61052afdde";

    private static readonly JsonSerializerOptions _jsonSerializerOptions = new()
    {
        PropertyNameCaseInsensitive = true,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
    };

    public MercadoPagoGateway(HttpClient httpClient, ILogger<MercadoPagoGateway> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
    }

    public async Task<PaymentResult> CreatePaymentAsync(
        PaymentInput input,
        CancellationToken cancellationToken)
    {
        const string CREATE_PAYMENT_PATH_TEMPLATE = "/v1/payments";
        const string IDEMPOTENCY_KEY = "X-Idempotency-Key";

        //var request = new HttpRequestMessage(HttpMethod.Post, CREATE_PAYMENT_PATH_TEMPLATE)
        //{
        //    Headers = { { IDEMPOTENCY_KEY, input.OrderId } },
        //    Content = CreateContent(input)
        //};      

        //var response = await _httpClient.SendAsync(request, cancellationToken);             
        
        using var httpClient = new HttpClient();

        var response = await httpClient.GetAsync(BASE_URL_MOCK, cancellationToken);
        var responseContent = await response.Content.ReadAsStringAsync(cancellationToken);

        if (response.IsSuccessStatusCode is false)
        {
            _logger.LogCritical(
                "Failed to create payment for order {OrderId}. Response: {ResponseContent}",
                input.OrderId,
                responseContent);

            throw new HttpRequestException($"Failed to create payment for order {input.OrderId}. Status code: {response.StatusCode}");
        }

        var mercadoPagoResponse = JsonSerializer.Deserialize<MercadoPagoPaymentResponse>(responseContent, _jsonSerializerOptions);

        return mercadoPagoResponse!.ToDomain();

    }

    private static StringContent CreateContent(PaymentInput input)
    {
        const string CONTENT_TYPE = "application/json";

        var requestContent = new MercadoPagoPaymentRequest
        {
            TransactionAmount = input.TotalPrice,
            PaymentMethodId = input.PaymentMethod.ToString().ToLower(),
            Metadata = new MercadoPagoMetadata
            {
                OrderNumber = input.OrderId
            },
            Payer = new MercadoPagoPayer
            {
                FirstName = input.CustomerName!.Split(' ').FirstOrDefault(),
                LastName = input.CustomerName!.Split(' ').LastOrDefault(),
                Email = input.CustomerEmail
            }
        };

        return new StringContent(
            JsonSerializer.Serialize(requestContent),
            Encoding.UTF8,
            CONTENT_TYPE);
    }
}
