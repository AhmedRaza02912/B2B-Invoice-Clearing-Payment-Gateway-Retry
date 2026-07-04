namespace InvoiceProcessor.Models;

// holds retry metadata for an invoice, remains so retries can resume after restart.

public class RetryState
{
    public Guid InvoiceId{get; init;}
    public int AttemptCount{get;set;}

    public TimeSpan CurrentBackOff{get;set;}

    public DateTime NextRetryAt{get;set;}
    public DateTime RetryDeadline{get; init;}
    public InvoiceStatus Status {get;set;}

    
}