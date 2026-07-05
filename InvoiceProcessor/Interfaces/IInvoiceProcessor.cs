using InvoiceProcessor.Models;

namespace InvoiceProcessor.Interfaces;

public interface IInvoiceProcessor
{
    Task ProcessAsync(
        Invoice invoice,
        CancellationToken cancellationToken = default);
}