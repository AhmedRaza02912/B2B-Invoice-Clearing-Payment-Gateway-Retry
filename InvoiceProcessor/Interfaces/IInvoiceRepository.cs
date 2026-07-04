using InvoiceProcessor.Models;

namespace InvoiceProcessor.Interfaces;

// Access to invoices.
// Read only collection will restrict any modifications to invoices.
public interface IInvoiceRepository
{
    Task<IReadOnlyCollection<Invoice>> GetAllAsync(
        CancellationToken cancellationToken = default
    );
}