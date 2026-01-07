using Business.Entities.Enums;

namespace Business.UseCases.Exceptions;

internal class PaymentStatusNotSupportedException : Exception
{
    private const string DEFAULT_MESSAGE = "Payment status '{0}' not supported.";

    internal PaymentStatusNotSupportedException(string paymentStatus)
        : base(string.Format(DEFAULT_MESSAGE, paymentStatus))
    {
    }

    internal static void ThrowIfPaymentStatusIsNotSupported(string paymentStatus)
    {
        var isInvalid =
            Enum.TryParse(paymentStatus, out PaymentStatus paymentStatusEnum) is false;

        if (isInvalid)
        {
            throw new PaymentStatusNotSupportedException(paymentStatus);
        }
    }
}
