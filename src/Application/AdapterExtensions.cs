using Adapter.Controllers;
using Adapter.Controllers.Interfaces;
using Adapter.Gateways.Repositories;
using Business.Gateways.Repositories.Interfaces;
using Business.UseCases;
using Business.UseCases.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace Adapter;

[ExcludeFromCodeCoverage]
public static class AdapterExtensions
{
    public static IServiceCollection InjectAdapterDependencies(this IServiceCollection services)
    {
        return services
            .RegisterUseCases()
            .RegisterControllers()
            .RegisterGateways();
    }

    private static IServiceCollection RegisterUseCases(this IServiceCollection services)
    {

        return services
         .AddSingleton<IPaymentUseCase, PaymentUseCase>();

    }

    private static IServiceCollection RegisterControllers(this IServiceCollection services)
    {
        return services
             .AddSingleton<IPaymentController, PaymentController>();
    }

    public static IServiceCollection RegisterGateways(this IServiceCollection services)
    {
        return services
            .AddSingleton<IPaymentRepository, PaymentGateway>();

    }
}
