using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MockPaymentGateway.Configuration;
using MockPaymentGateway.Contracts;
using MockPaymentGateway.Models;

namespace MockPaymentGateway.Services;

public sealed class PaymentSimulationService : IPaymentSimulationService
{
    private readonly PaymentSimulationOptions _options;
    private readonly ILogger<PaymentSimulationService> _logger;
    private readonly Random _random = Random.Shared;

    public PaymentSimulationService(
        IOptions<PaymentSimulationOptions> options,
        ILogger<PaymentSimulationService> logger)
    {
        _options = options.Value;
        _logger = logger;
    }

    public PaymentResult ProcessPayment(PaymentRequest request)
    {
        var roll = _random.Next(1, 101);

        PaymentResult result;

        if (roll <= _options.SuccessRate)
        {
            result = new PaymentResult
            {
                Outcome = PaymentOutcome.Success,
                Message = "Payment processed successfully."
            };
        }
        else if (roll <= _options.SuccessRate + _options.InternalServerErrorRate)
        {
            result = new PaymentResult
            {
                Outcome = PaymentOutcome.InternalServerError,
                Message = "Temporary server error."
            };
        }
        else
        {
            result = new PaymentResult
            {
                Outcome = PaymentOutcome.RateLimited,
                Message = "Too many requests."
            };
        }

        _logger.LogInformation(
            "Invoice {InvoiceId} | Amount: {Amount} | Outcome: {Outcome}",
            request.InvoiceId,
            request.Amount,
            result.Outcome);

        return result;
    }
}