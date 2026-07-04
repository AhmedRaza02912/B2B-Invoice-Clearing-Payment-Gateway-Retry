using MockPaymentGateway.Models;
namespace MockPaymentGateway.Models;

// Response by the mock payment gateway.

public sealed class PaymentResult
{
    public PaymentOutcome Outcome{get; init;} 
    public string Message{get; init;} = string.Empty;
}