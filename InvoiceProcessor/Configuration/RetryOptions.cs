namespace InvoiceProcessor.Configuration;

public sealed class RetryOptions
{
    public const string SectionName ="RetryOptions";
    public int InitialDelaySeconds{get; init;}
    public int MaximumDelaySeconds{get;init;}
    public int RetryWindowMinutes{get; init;}
}