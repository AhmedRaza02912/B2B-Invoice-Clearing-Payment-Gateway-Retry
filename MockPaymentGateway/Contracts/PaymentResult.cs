namespace MockPaymentGateway.Contracts;

// Response by the mock payment gateway.

public sealed class PaymentResult
{
    public bool Success{get; init;}
    public string Message{get; init;} = string.Empty;
}