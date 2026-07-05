namespace InvoiceProcessor.Models;

// Result returned by payment gateway

public sealed class PaymentResponse
{
    public PaymentResult Result { get; init; }
    public string Message { get; init; } = string.Empty;

}