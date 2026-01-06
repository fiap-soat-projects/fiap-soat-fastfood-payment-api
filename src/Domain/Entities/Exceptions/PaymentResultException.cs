using Business.Exceptions;

namespace Business.Entities.Exceptions;

public class PaymentResultException : InvalidEntityPropertyException<PaymentResult>
{
    protected PaymentResultException(string propertyName) : base(propertyName)
    {

    }
}
