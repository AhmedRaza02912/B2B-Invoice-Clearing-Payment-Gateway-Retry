using InvoiceProcessor.Interfaces;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace InvoiceProcessor.Services;

public sealed class InvoiceProcessingService : BackgroundService
{
    private readonly IInvoiceRepository _invoiceRepository;
    private readonly IPaymentGateway _paymentGateway;
    private readonly ILogger<InvoiceProcessingService> _logger;

    public InvoiceProcessingService(
        IInvoiceRepository invoiceRepository,
        IPaymentGateway paymentGateway,
        ILogger<InvoiceProcessingService> logger
    )
    {
        _invoiceRepository = invoiceRepository;
        _paymentGateway = paymentGateway;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(
        CancellationToken stoppingToken
    )
    {
        var invoices = await _invoiceRepository.LoadInvoicesAsync(stoppingToken);
        _logger.LogInformation("Loaded {Count} invoice", invoices.Count);

        foreach(var invoice in invoices)
        {
            var response = await _paymentGateway.ProcessPaymentAsync(
                invoice, stoppingToken
            );
            _logger.LogInformation("Invoice {InvoiceId} -> {Result}",
            invoice.Id, response.Result);
        }
    }

}