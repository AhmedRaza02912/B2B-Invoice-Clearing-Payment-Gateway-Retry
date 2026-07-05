using InvoiceProcessor.Models;

namespace InvoiceProcessor.Interfaces;

// Access to invoices.
// Read only list will restrict any modifications to invoices.
public interface IInvoiceRepository
{
    Task<IReadOnlyList<Invoice>> LoadInvoicesAsync(
        CancellationToken cancellationToken = default
    );
}