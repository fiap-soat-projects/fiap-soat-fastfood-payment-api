using Adapter.Controllers.DTOs;
//using Adapter.Controllers.DTOs.Filters;
using Adapter.Controllers.Interfaces;
using Adapter.Presenters;
using Business.Entities;
using Business.Entities.Enums;
using Business.Gateways.Clients.DTOs;
using Business.UseCases.Exceptions;
using Business.UseCases.Interfaces;

namespace Adapter.Controllers;

internal class PaymentController : IPaymentController
{
    //private readonly IOrderUseCase _orderUseCase;
    //private readonly IMenuItemUseCase _menuItemUseCase;
    //private readonly IInventoryUseCase _inventoryUseCase;
    private readonly ITransactionUseCase _paymentUseCase;

    public PaymentController(
        //IOrderUseCase orderUseCase,
        //IMenuItemUseCase menuItemUseCase,
        //IInventoryUseCase inventoryUseCase,
        ITransactionUseCase paymentUseCase)
    {
        //_orderUseCase = orderUseCase;
        //_menuItemUseCase = menuItemUseCase;
        //_inventoryUseCase = inventoryUseCase;
        _paymentUseCase = paymentUseCase;
    }

    //public async Task<OrderPaginatedPresenter> GetAllAsync(OrderFilter filter, CancellationToken cancellationToken)
    //{
    //    var status = ParseOrderStatus(filter.Status!);

    //    var orderPage = await _orderUseCase.GetAllAsync(cancellationToken, status, filter.Page, filter.Size);

    //    return new OrderPaginatedPresenter(orderPage);
    //}

    //public async Task<OrderPaginatedPresenter> GetActiveAsync(OrderFilter filter, CancellationToken cancellationToken)
    //{
    //    var orderPage = await _orderUseCase.GetActiveAsync(cancellationToken, filter.Page, filter.Size);

    //    return new OrderPaginatedPresenter(orderPage);
    //}

    //public async Task<OrderPresenter> GetByIdAsync(string id, CancellationToken cancellationToken)
    //{
    //    var order = await _orderUseCase.GetByIdAsync(id, cancellationToken);

    //    OrderNotFoundException.ThrowIfNull(order, id);

    //    return new OrderPresenter(order);
    //}

    //public async Task<string> CreateAsync(CreateRequest request, CancellationToken cancellationToken)
    //{
    //    var orderItems = new List<OrderItem>();

    //    foreach (var item in request.Items)
    //    {
    //        var menuItem = await _menuItemUseCase.GetByIdAsync(item.Id!, cancellationToken);

    //        orderItems.Add(new OrderItem
    //        (
    //            menuItem.Id!,
    //            menuItem.Name!,
    //            menuItem.Category,
    //            menuItem.Price,
    //            item.Amount
    //        ));
    //    }

    //    var order = new Order
    //    (
    //        request.CustomerId,
    //        request.CustomerName,
    //        orderItems
    //    );

    //    var orderId = await _orderUseCase.CreateAsync(order, cancellationToken);

    //    return orderId;
    //}

    //public async Task<OrderPresenter> UpdateStatusAsync(string id, UpdateStatusRequest updateStatusRequest, CancellationToken cancellationToken)
    //{
    //    InvalidOrderStatusException.ThrowIfNullOrEmpty(updateStatusRequest.Status);

    //    var orderStatus = ParseOrderStatus(updateStatusRequest.Status!);

    //    var order = await _orderUseCase.UpdateStatusAsync(id, orderStatus, cancellationToken);

    //    if (orderStatus == OrderStatus.Finished)
    //    {
    //        _inventoryUseCase.GenerateAuditLog(order, DateTime.UtcNow);
    //    }

    //    return new OrderPresenter(order);
    //}

    //public async Task DeleteAsync(string id, CancellationToken cancellationToken)
    //{
    //    await _orderUseCase.DeleteAsync(id, cancellationToken);
    //}

    public async Task<CheckoutPresenter> CheckoutAsync(CheckoutRequest request, CancellationToken cancellationToken)
    {

        PaymentMethodNotSupportedException.ThrowIfPaymentMethodIsNotSupported(request.PaymentType!);
        _ = Enum.TryParse(request.PaymentType, out PaymentMethod paymentMethod);

        var customerId = !string.IsNullOrWhiteSpace(request.CustomerId) ? request.CustomerId : "NoUser";
        var customerName = !string.IsNullOrWhiteSpace(request.CustomerName) ? request.CustomerName : "fakeUserName";
        var customerEmail = !string.IsNullOrWhiteSpace(request.CustomerEmail) ? request.CustomerEmail : "fakeemail@fake.com";

        var checkoutInput = new CheckoutInput
        (
            customerId!,
            customerName!,
            customerEmail!,
            request.OrderId!,
            request.TotalPrice
        );

        var orderPaymentCheckout = await _paymentUseCase.CheckoutAsync(checkoutInput, paymentMethod, cancellationToken);

        return new CheckoutPresenter(orderPaymentCheckout);
    }

    public async Task ConfirmPaymentAsync(string id, CancellationToken cancellationToken)
    {
        await _paymentUseCase.ConfirmPaymentAsync(id, cancellationToken);
    }

    public async Task ProcessPaymentAsync(PaymentWebhook request, CancellationToken cancellationToken)
    {
        var payment = new Payment
        {
            Id = request.PaymentId,
            Status = request.PaymentStatus,
            Method = request.PaymentMethod
        };

        await _paymentUseCase.ProcessPaymentAsync(request.OrderId!, payment, cancellationToken);
    }

    //private static OrderStatus ParseOrderStatus(string text)
    //{
    //    _ = Enum.TryParse(text, out OrderStatus status);

    //    return status;
    //}
}
