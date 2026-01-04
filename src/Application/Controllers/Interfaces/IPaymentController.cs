using Adapter.Controllers.DTOs;
//using Adapter.Controllers.DTOs.Filters;
using Adapter.Presenters;

namespace Adapter.Controllers.Interfaces;

public interface IPaymentController
{
    //Task<string> CreateAsync(CreateRequest request, CancellationToken cancellationToken);
    //Task<OrderPresenter> GetByIdAsync(string id, CancellationToken cancellationToken);
    //Task<OrderPaginatedPresenter> GetAllAsync(OrderFilter filter, CancellationToken cancellationToken);
    //Task<OrderPaginatedPresenter> GetActiveAsync(OrderFilter filter, CancellationToken cancellationToken);
    Task<CheckoutPresenter> CheckoutAsync(string id, CheckoutRequest request, CancellationToken cancellationToken);
    Task ConfirmPaymentAsync(string id, CancellationToken cancellationToken);
    Task ProcessPaymentAsync(PaymentWebhook request, CancellationToken cancellationToken);
    //Task<OrderPresenter> UpdateStatusAsync(string id, UpdateStatusRequest updateStatusRequest, CancellationToken cancellationToken);
    //Task DeleteAsync(string id, CancellationToken cancellationToken);
}
