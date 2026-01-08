using Adapter.Controllers;
using Business.UseCases.Interfaces;
using NSubstitute;

namespace Adapter.Tests.StepDefinitions
{
    public abstract class PaymentControllerTestsBase
    {
        internal readonly IPaymentUseCase _paymentUseCase;
        internal readonly PaymentController _sut;

        protected PaymentControllerTestsBase()
        {
            _paymentUseCase = Substitute.For<IPaymentUseCase>();

            _sut = new PaymentController(
                _paymentUseCase);
        }
    }
}
