using Business.Entities;
using Business.Entities.Enums;
using Business.Gateways.Repositories.Interfaces;
using Infrastructure.Entities;
using Infrastructure.Repositories.Interfaces;

namespace Adapter.Gateways.Repositories;

internal class PaymentGateway : IPaymentRepository
{
    private readonly IPaymentMongoDbRepository _paymentMongoDbRepository;

    public PaymentGateway(IPaymentMongoDbRepository paymentMongoDbRepository)
    {
        _paymentMongoDbRepository = paymentMongoDbRepository;
    }

    public Task<string> CreateAsync(Payment payment, CancellationToken cancellationToken)
    {
        var orderMongoDb = new PaymentMongoDb(payment);
        return _paymentMongoDbRepository.InsertOneAsync(orderMongoDb, cancellationToken);
    }


    public async Task<Payment?> GetByIdAsync(string id, CancellationToken cancellationToken)
    {
        var orderMongoDB = await _paymentMongoDbRepository.GetByIdAsync(id, cancellationToken);
        return orderMongoDB?.ToDomain();
    }


    public async Task UpdateStatusAsync(string id, PaymentStatus paymentStatus, CancellationToken cancellationToken)
    {
        await _paymentMongoDbRepository.UpdateStatusAsync(id, paymentStatus, cancellationToken);
    }
}
