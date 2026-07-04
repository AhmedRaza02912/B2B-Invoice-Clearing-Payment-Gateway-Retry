using Microsoft.Extensions.Options;

namespace MockPaymentGateway.Configuration;

// RThis code handles if simulation rates dont add upto 100
public sealed class PaymentSimulationOptionsValidator : IValidateOptions<PaymentSimulationOptions>
{
    public ValidateOptionsResult Validate(
        string? Name,
        PaymentSimulationOptions options
    )
    {
        if(options.SuccessRate < 0 || 
        options.InternalServerErrorRate < 0 ||
         options.RateLimitRate < 0)
        {
            return ValidateOptionsResult.Fail(
                "Either one or more simulation rates were negative."
            );
        }

        var total = 
        options.SuccessRate + options.InternalServerErrorRate + options.RateLimitRate;

        if(total != 100)
        {
            return ValidateOptionsResult.Fail(
                "Simulation rates must add upto 100."
            );
        }
        return ValidateOptionsResult.Success;
    }
}