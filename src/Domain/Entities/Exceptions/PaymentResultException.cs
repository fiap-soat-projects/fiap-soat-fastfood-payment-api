using Business.Exceptions;
using System.Diagnostics.CodeAnalysis;

namespace Business.Entities.Exceptions;

[ExcludeFromCodeCoverage]
public class PaymentResultException : InvalidEntityPropertyException<PaymentResult>
{
    protected PaymentResultException(string propertyName) : base(propertyName)
    {

    }
}
