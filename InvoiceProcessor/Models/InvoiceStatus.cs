namespace InvoiceProcessor.Models;

// Current state of invoice
// I used enum over string matching to mitigate typos
public enum InvoiceStatus
{
    Pending,
    Paid,
    Retrying,
    Failed
}