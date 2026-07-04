using Microsoft.Extensions.Options;
using MockPaymentGateway.Configuration;
using MockPaymentGateway.Contracts;
using MockPaymentGateway.Models;

namespace MockPaymentGateway.Services;

public sealed class PaymentSimulationService : IPaymentSimulationService
{
    private readonly PaymentSimulationOptions _options;
    
    // Thread safe random value
    private readonly Random _random = Random.Shared; 

    public PaymentSimulationService(
        IOptions<PaymentSimulationOptions> options
    )
    {
        _options = options.Value;
    }

    public PaymentResult ProcessPayment(PaymentRequest request)
    {
        var roll = _random.Next(1, 101);

        if(roll <= _options.SuccessRate)
        {
            return new PaymentResult
            {
                Outcome = PaymentOutcome.Success,
                Message = "Payment processed successfully"
            };
        }

        if(roll <= _options.SuccessRate + _options.InternalServerErrorRate)
        {
            return new PaymentResult
            {
                Outcome = PaymentOutcome.InternalServerError,
                Message = "Temporary server error."
            };
        }

        return new PaymentResult
        {
            Outcome = PaymentOutcome.RateLimited,
            Message = "Too many requests!"
        };
    }
}