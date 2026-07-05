using InvoiceProcessor.Configuration;
using InvoiceProcessor.Interfaces;
using Microsoft.Extensions.Options;

namespace InvoiceProcessor.Services;

public sealed class ExponentialRetryPolicy : IRetryPolicy
{
    private readonly RetryOptions _options;

    public ExponentialRetryPolicy(IOptions<RetryOptions> options)
    {
        _options = options.Value;
    }

    public TimeSpan GetDelay(int attempt)
    {
        // attempt 1 -> 1 second
        // attempt 2 -> 2 seconds
        // attempt 3 -> 4 seconds

        var seconds = _options.InitialDelaySeconds * Math.Pow(2, attempt - 1);

        seconds = Math.Min(seconds, _options.MaximumDelaySeconds);

        return TimeSpan.FromSeconds(seconds);
    }
}