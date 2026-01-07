using Business.Entities;
using Business.Entities.Enums;
using MongoDB.Bson.Serialization.Attributes;

namespace Infrastructure.Entities;

[BsonIgnoreExtraElements]
[BsonDiscriminator("payment")]
internal class PaymentMongoDb : MongoEntity
{
    public string? OrderId { get; set; }
    public string? CustomerId { get; set; }
    public string? CustomerName { get; set; }
    public string? CustomerEmail { get; set; }
    public decimal TotalPrice { get; set; }
    public string? QrCode { get; set; }
    public string? QrCodeBase64 { get; set; }
    public PaymentMethod PaymentMethod { get; set; }
    public PaymentStatus PaymentStatus { get; set; }
    public string? PaymentResponse { get; set; }


    public PaymentMongoDb()
    {

    }

    internal PaymentMongoDb(Payment payment)
    {

        OrderId = payment.OrderId;
        CustomerId = payment.CustomerId;
        CustomerName = payment.CustomerName;
        CustomerEmail = payment.CustomerEmail;
        TotalPrice = payment.TotalPrice;
        QrCode = payment.QrCode;
        QrCodeBase64 = payment.QrCodeBase64;
        PaymentMethod = payment.PaymentMethod;
        PaymentStatus = payment.PaymentStatus;
        PaymentResponse = payment.PaymentResponse;
    }

    internal Payment ToDomain()
    {

        return new Payment(
            Id,
            OrderId,
            CustomerId,
            CustomerName,
            CustomerEmail,
            TotalPrice,
            QrCode,
            QrCodeBase64,
            PaymentMethod,
            PaymentStatus,
            PaymentResponse)
        {

        };
    }
}
