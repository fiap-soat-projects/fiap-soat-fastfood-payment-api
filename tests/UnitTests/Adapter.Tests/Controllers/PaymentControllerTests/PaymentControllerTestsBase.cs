using Adapter.Controllers;
using Adapter.Controllers.Interfaces;
using Business.UseCases.Interfaces;
using NSubstitute;

namespace Adapter.Tests.Controllers.PaymentControllerTests;

public abstract class PaymentControllerTestsBase
{
    internal readonly IPaymentUseCase _paymentUseCase;
    internal readonly IPaymentController _sut;

    protected PaymentControllerTestsBase()
    {
        _paymentUseCase = Substitute.For<IPaymentUseCase>();
        _sut = new PaymentController(_paymentUseCase);
    }
}
