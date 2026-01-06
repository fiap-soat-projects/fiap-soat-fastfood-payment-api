using Business.Entities.Enums;
using Infrastructure.Entities;
using Infrastructure.MongoDb.Contexts.Interfaces;
using Infrastructure.Repositories.Interfaces;
using MongoDB.Driver;

namespace Infrastructure.Repositories;

internal class PaymentMongoDbRepository : BaseRepository<PaymentMongoDb>, IPaymentMongoDbRepository
{
    public PaymentMongoDbRepository(IMongoContext mongoContext) : base(mongoContext)
    {

    }

    public async Task<string> InsertOneAsync(PaymentMongoDb order, CancellationToken cancellationToken)
    {
        await _collection.InsertOneAsync(order, null, cancellationToken);

        return order.Id;
    }

    public async Task<PaymentMongoDb?> GetByIdAsync(string id, CancellationToken cancellationToken)
    {
        var filter = Builders<PaymentMongoDb>.Filter.Eq(entity => entity.Id, id);

        var order = await _collection.Find(filter).FirstOrDefaultAsync(cancellationToken);

        return order;
    }



    public async Task UpdateStatusAsync(string id, PaymentStatus status, CancellationToken cancellationToken)
    {
        var filter = Builders<PaymentMongoDb>.Filter.Eq(entity => entity.Id, id);

        var update = Builders<PaymentMongoDb>.Update
            .Set(entity => entity.PaymentStatus, status);


        var options = new FindOneAndUpdateOptions<PaymentMongoDb>
        {
            ReturnDocument = ReturnDocument.After
        };

        _ = await _collection.FindOneAndUpdateAsync(filter, update, options, cancellationToken: cancellationToken);
    }
}
