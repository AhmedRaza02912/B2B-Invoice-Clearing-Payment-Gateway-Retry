using InvoiceProcessor.Interfaces;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using InvoiceProcessor.Configuration;
using Microsoft.Extensions.Options;

namespace InvoiceProcessor.Services;

public sealed class InvoiceProcessingService : BackgroundService
{
    private readonly IInvoiceRepository _invoiceRepository;
    private readonly IPaymentGateway _paymentGateway;
    private readonly ILogger<InvoiceProcessingService> _logger;
    private readonly RetryOptions _retryOptions;
    private readonly IInvoiceProcessor _invoiceProcessor;

    public InvoiceProcessingService(
        IInvoiceRepository invoiceRepository,
        IPaymentGateway paymentGateway,
        ILogger<InvoiceProcessingService> logger,
         IOptions<RetryOptions> retryOptions,
         IInvoiceProcessor invoiceProcessor
    )
    {
        _invoiceRepository = invoiceRepository;
        _paymentGateway = paymentGateway;
        _logger = logger;
        _retryOptions = retryOptions.Value;
        _invoiceProcessor = invoiceProcessor;
    }

    protected override async Task ExecuteAsync(
    CancellationToken stoppingToken)
    {
        var invoices = await _invoiceRepository.LoadInvoicesAsync(stoppingToken);

        _logger.LogInformation(
            "Loaded {Count} invoices.",
            invoices.Count);

        var processingTasks = invoices
         .Select(invoice =>
             _invoiceProcessor.ProcessAsync(invoice, stoppingToken))
         .ToList();

        await Task.WhenAll(processingTasks);
    }

}