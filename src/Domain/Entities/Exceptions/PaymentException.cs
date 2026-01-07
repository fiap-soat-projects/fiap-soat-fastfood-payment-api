using Business.Exceptions;
using System.Diagnostics.CodeAnalysis;

namespace Business.Entities.Exceptions;

[ExcludeFromCodeCoverage]
public class PaymentException : InvalidEntityPropertyException<Payment>
{
    public PaymentException(string propertyName) : base(propertyName)
    {

    }
}
