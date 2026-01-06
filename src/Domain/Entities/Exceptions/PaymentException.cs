using Business.Exceptions;

namespace Business.Entities.Exceptions;

public class PaymentException : InvalidEntityPropertyException<Payment>
{
    public PaymentException(string propertyName) : base(propertyName)
    {

    }
}
