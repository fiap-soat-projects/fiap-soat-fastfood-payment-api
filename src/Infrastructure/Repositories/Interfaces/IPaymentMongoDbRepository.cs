using Business.Entities.Enums;
using Infrastructure.Entities;

namespace Infrastructure.Repositories.Interfaces;

internal interface IPaymentMongoDbRepository
{
    Task<string> InsertOneAsync(PaymentMongoDb payment, CancellationToken cancellationToken);
    Task<PaymentMongoDb?> GetByIdAsync(string id, CancellationToken cancellationToken);
    Task UpdateStatusAsync(string id, PaymentStatus status, CancellationToken cancellationToken);
}
