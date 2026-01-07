using Business.Entities.Exceptions;

namespace Business.Entities;

public class PaymentResult
{
    private string? _id;
    private string? _paymentMethod;
    private string? _paymentStatus;
    private string? _qrCode;
    private string? _qrCodeBase64;
    private decimal _amount;
    private string? _paymentResponse;


    public string Id
    {
        get => _id!;
        set
        {
            PaymentResultException.ThrowIfNullOrWhiteSpace(value, nameof(Id));
            _id = value;
        }
    }

    public required string PaymentMethod
    {
        get => _paymentMethod!;
        set
        {
            PaymentResultException.ThrowIfNullOrWhiteSpace(value, nameof(PaymentMethod));

            _paymentMethod = value;
        }
    }

    public required string PaymentStatus
    {
        get => _paymentStatus!;
        set
        {
            PaymentResultException.ThrowIfNullOrWhiteSpace(value, nameof(PaymentStatus));

            _paymentStatus = value;
        }
    }


    public required string QrCode
    {
        get => _qrCode!;
        set
        {
            PaymentResultException.ThrowIfNullOrWhiteSpace(value, nameof(PaymentMethod));

            _qrCode = value;
        }
    }

    public required string QrCodeBase64
    {
        get => _qrCodeBase64!;
        set
        {
            PaymentResultException.ThrowIfNullOrWhiteSpace(value, nameof(PaymentMethod));

            _qrCodeBase64 = value;
        }
    }

    public required decimal Amount
    {
        get => _amount;
        set
        {
            PaymentResultException.ThrowIfIsEqualOrLowerThanZero(value, nameof(PaymentMethod));

            _amount = value;
        }
    }

    public required string PaymentResponse
    {
        get => _paymentResponse!;
        set
        {
            PaymentResultException.ThrowIfNullOrWhiteSpace(value, nameof(PaymentResponse));

            _paymentResponse = value;
        }
    }
}
