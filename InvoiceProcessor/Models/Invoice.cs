namespace InvoiceProcessor.Models;

public sealed class Invoice
{
    public Guid Id { get; init; }

    public string CustomerName { get; init; } = string.Empty;

    public decimal Amount { get; init; }

    public InvoiceStatus Status { get; set; } = InvoiceStatus.Pending;

   // Retry Metadata
    public int AttemptCount { get; set; }

    public DateTime CreatedAt { get; init; } = DateTime.UtcNow;

    public DateTime RetryWindowEndsAt { get; set; }

    public DateTime? NextRetryAt { get; set; }
}