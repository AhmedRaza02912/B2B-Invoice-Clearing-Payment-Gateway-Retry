using InvoiceProcessor.Models;

namespace InvoiceProcessor.Interfaces;

// Retry info for invoices.

public interface IRetryStateRepository
{
    Task SaveAsync(
        RetryState retryState,
        CancellationToken cancellationToken = default
    );

    Task<RetryState?> GetAsync(
        Guid invoiceId, 
        CancellationToken cancellationToken = default
    );

    Task<IReadOnlyCollection<RetryState>> GetPendingRetriesAsync(
        CancellationToken cancellationToken = default
    );

    // When status = paid remove the retry info.
    Task DeleteAsync(
        Guid invoiceId,
        CancellationToken cancellationToken = default
    );
}