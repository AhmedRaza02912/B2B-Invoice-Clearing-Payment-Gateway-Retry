namespace InvoiceProcessor.Models;

public enum PaymentResult
{
    Success,
    InternalServerError,
    RateLimited
}