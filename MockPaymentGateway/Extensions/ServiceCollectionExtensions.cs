using Microsoft.Extensions.Options;
using MockPaymentGateway.Configuration;
using MockPaymentGateway.Services;

namespace MockPaymentGateway.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPaymentSimulation(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.AddOptions<PaymentSimulationOptions>()
        .Bind(configuration.GetSection(PaymentSimulationOptions.SectionName))
        .ValidateOnStart();

        services.AddSingleton<IValidateOptions<PaymentSimulationOptions>, PaymentSimulationOptionsValidator>();

        services.AddSingleton<IPaymentSimulationService, PaymentSimulationService>();

        return services;
    }
}