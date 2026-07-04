namespace MockPaymentGateway.Contracts;

// Represents a payment request from invoice processor

public sealed class PaymentRequest
{
    public Guid InvoiceId{get; init;}
    public decimal Amount{get; init;}
}
