namespace MockPaymentGateway.Models;

// Outcome of a payment request.

public enum PaymentOutcome
{
    Success,
    InternalServerError,
    RateLimited
}