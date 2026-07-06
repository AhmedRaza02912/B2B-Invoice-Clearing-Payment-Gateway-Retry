using InvoiceProcessor.Interfaces;
using InvoiceProcessor.Models;
using Microsoft.Extensions.Logging;
using InvoiceProcessor.Configuration;
using Microsoft.Extensions.Options;

namespace InvoiceProcessor.Services;

public sealed class InvoiceRetryProcessor : IInvoiceProcessor
{
    private readonly IPaymentGateway _paymentGateway;
    private readonly ILogger<InvoiceRetryProcessor> _logger;
    private readonly IRetryPolicy _retryPolicy;
    private readonly RetryOptions _retryOptions;
    public InvoiceRetryProcessor(
        IPaymentGateway paymentGateway,
        IRetryPolicy retryPolicy,
        IOptions<RetryOptions> retryOptions,
        ILogger<InvoiceRetryProcessor> logger)
    {
        _paymentGateway = paymentGateway;
        _retryPolicy = retryPolicy;
        _retryOptions = retryOptions.Value;
        _logger = logger;
    }

    public async Task ProcessAsync(
    Invoice invoice,
    CancellationToken cancellationToken = default)
{
    invoice.Status = InvoiceStatus.Pending;
    invoice.RetryWindowEndsAt = DateTime.UtcNow.AddMinutes(_retryOptions.RetryWindowMinutes);

    while (DateTime.UtcNow < invoice.RetryWindowEndsAt)
    {
        invoice.AttemptCount++;

        var response = await _paymentGateway.ProcessPaymentAsync(
            invoice,
            cancellationToken);

        if (response.Result == PaymentResult.Success)
        {
            invoice.Status = InvoiceStatus.Paid;

            _logger.LogInformation(
                "Invoice {InvoiceId} paid successfully after {Attempts} attempt(s).",
                invoice.Id,
                invoice.AttemptCount);

            return;
        }

        invoice.Status = InvoiceStatus.Retrying;

        var delay = _retryPolicy.GetDelay(invoice.AttemptCount);

        invoice.NextRetryAt = DateTime.UtcNow.Add(delay);

        _logger.LogWarning(
    "[{Timestamp}] Invoice {InvoiceId} | Attempt {Attempt} failed ({Outcome}). Next retry scheduled at {NextRetry}.",
    DateTime.UtcNow,
    invoice.Id,
    invoice.AttemptCount,
    response.Result,
    invoice.NextRetryAt);

        await Task.Delay(delay, cancellationToken);
    }

    invoice.Status = InvoiceStatus.Failed;

    _logger.LogError(
        "Invoice {InvoiceId} failed after retry window expired.",
        invoice.Id);
}
}