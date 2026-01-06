using Business.Entities.Enums;
using Business.Entities.Exceptions;
using Business.Entities.Interfaces;
using Business.UseCases.Exceptions;

namespace Business.Entities;

public class Payment : IAggregateRoot
{
    private string? _id;
    private string? _orderId;
    private string? _customerId;
    private string? _customerName;
    private string? _customerEmail;
    private decimal _totalPrice;
    private string? _qrCode;
    private string? _qrCodeBase64;
    private PaymentMethod _paymentMethod;
    private PaymentStatus _paymentStatus;
    private string? _paymentResponse;

    public string? Id
    {
        get => _id!;
        set
        {
            PaymentException.ThrowIfNullOrWhiteSpace(value, nameof(Id));

            _id = value;
        }
    }

    public string? OrderId
    {
        get => _orderId;
        set => _orderId = value;
    }

    public string? CustomerId
    {
        get => _customerId;
        set => _customerId = value;
    }

    public string? CustomerName
    {
        get => _customerName;
        set => _customerName = value;
    }

    public string? CustomerEmail
    {
        get => _customerEmail;
        set => _customerEmail = value;
    }

    public string? QrCode
    {
        get => _qrCode;
        set => _qrCode = value;
    }

    public string? QrCodeBase64
    {
        get => _qrCodeBase64;
        set => _qrCodeBase64 = value;
    }

    public PaymentMethod PaymentMethod
    {
        get => _paymentMethod;
        set
        {
            ValidatePaymentMethod(value);
            _paymentMethod = value;
        }
    }

    public PaymentStatus PaymentStatus
    {
        get => _paymentStatus;
        set
        {
            ValidatePaymentStatus(value);
            _paymentStatus = value;
        }
    }



    public string? PaymentResponse
    {
        get => _paymentResponse;
        set => _paymentResponse = value;

    }

    public decimal TotalPrice
    {
        get => _totalPrice;
        set
        {
            PaymentException.ThrowIfIsEqualOrLowerThanZero(value, nameof(TotalPrice));

            _totalPrice = value;
        }
    }


    internal Payment(
        string? id,
        string? orderId,
        string? customerId,
        string? customerName,
        string? customerEmail,
        decimal totalPrice,
        string? qrCode,
        string? qrCodeBase64,
        PaymentMethod paymentMethod,
        PaymentStatus paymentStatus,
        string? paymentResponse
        )
    {
        Id = id;
        OrderId = orderId;
        CustomerId = customerId;
        CustomerName = customerName;
        CustomerEmail = customerEmail;
        TotalPrice = totalPrice;
        QrCode = qrCode;
        QrCodeBase64 = qrCodeBase64;
        PaymentMethod = paymentMethod;
        PaymentStatus = paymentStatus;
        PaymentResponse = paymentResponse;
    }

    internal Payment(
        string? orderId,
        string? customerId,
        string? customerName,
        string? customerEmail,
        decimal totalPrice,
        string? qrCode,
        string? qrCodeBase64,
        PaymentMethod paymentMethod,
        PaymentStatus paymentStatus,
        string? paymentResponse
        )
    {
        OrderId = orderId;
        CustomerId = customerId;
        CustomerName = customerName;
        CustomerEmail = customerEmail;
        TotalPrice = totalPrice;
        QrCode = qrCode;
        QrCodeBase64 = qrCodeBase64;
        PaymentMethod = paymentMethod;
        PaymentStatus = paymentStatus;
        PaymentResponse = paymentResponse;
    }

    private static void ValidatePaymentMethod(PaymentMethod paymentMethod)
    {
        var isInvalidPaymentMethod = !Enum.IsDefined(typeof(PaymentMethod), paymentMethod) || paymentMethod == PaymentMethod.None;

        if (isInvalidPaymentMethod)
        {
            throw new PaymentMethodNotSupportedException(nameof(PaymentMethod));
        }
    }

    private static void ValidatePaymentStatus(PaymentStatus paymentStatus)
    {
        var isInvalidPaymentStatus = !Enum.IsDefined(typeof(PaymentStatus), paymentStatus) || paymentStatus == PaymentStatus.None;

        if (isInvalidPaymentStatus)
        {
            throw new PaymentStatusNotSupportedException(nameof(PaymentStatus));
        }
    }


}
