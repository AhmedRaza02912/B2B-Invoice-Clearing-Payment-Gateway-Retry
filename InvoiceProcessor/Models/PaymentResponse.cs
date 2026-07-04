namespace InvoiceProcessor.Models;

// Result returned by payment gateway

public class PaymentResponse
{
    public bool isSuccess{get; init;}
    public int StatusCode{get; init;}
    public string Message{get;init;} = string.Empty;
    
}