namespace InvoiceProcessor.Interfaces;

// Calculate next retry delay.

public interface IRetryPolicy
{
    TimeSpan GetDelay(int attemptNumber);
}