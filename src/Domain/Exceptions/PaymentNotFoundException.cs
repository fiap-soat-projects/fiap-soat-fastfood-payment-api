using Business.Entities;
using System.Diagnostics.CodeAnalysis;

namespace Business.Exceptions;

[ExcludeFromCodeCoverage]
public class PaymentNotFoundException : EntityNotFoundException<Payment>
{
    protected PaymentNotFoundException(string id) : base(id)
    {

    }
}

