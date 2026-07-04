namespace InvoiceProcessor.Models;

// An invoice that needs clearence.

public class Invoice
{
    public Guid id{get; init;} // unique to invoice

    public string CustomerName {get; init;} = string.Empty;

    public decimal Amount{get; init;}

    public InvoiceStatus Status{get; set;} = InvoiceStatus.Pending;
}