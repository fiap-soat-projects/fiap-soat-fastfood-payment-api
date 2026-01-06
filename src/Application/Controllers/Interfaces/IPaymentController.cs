using Adapter.Controllers.DTOs;
using Adapter.Presenters;

namespace Adapter.Controllers.Interfaces;

public interface IPaymentController
{
    Task<PaymentPresenter> CreateAsync(PaymentRequest request, CancellationToken cancellationToken);
    Task<PaymentPresenter> GetAsync(string id, CancellationToken cancellationToken);
    Task ConfirmAsync(string id, CancellationToken cancellationToken);
}
