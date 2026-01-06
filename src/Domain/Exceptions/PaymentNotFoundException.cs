using Business.Entities;

namespace Business.Exceptions;

public class PaymentNotFoundException : EntityNotFoundException<Payment>
{
    protected PaymentNotFoundException(string id) : base(id)
    {

    }
}

