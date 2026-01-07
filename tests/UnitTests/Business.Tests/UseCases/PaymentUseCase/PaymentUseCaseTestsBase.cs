using Business.Gateways.Clients.Interfaces;
using Business.Gateways.Repositories.Interfaces;
using NSubstitute;

namespace Business.Tests.UseCases.PaymentUseCase;

public abstract class PaymentUseCaseTestsBase
{
    internal readonly IMercadoPagoClient _mercadoPagoClient;
    internal readonly IPaymentRepository _paymentRepository;
    internal readonly Business.UseCases.PaymentUseCase _sut;

    protected PaymentUseCaseTestsBase()
    {
        _mercadoPagoClient = Substitute.For<IMercadoPagoClient>();
        _paymentRepository = Substitute.For<IPaymentRepository>();
        _sut = new Business.UseCases.PaymentUseCase(_mercadoPagoClient, _paymentRepository);
    }
}
