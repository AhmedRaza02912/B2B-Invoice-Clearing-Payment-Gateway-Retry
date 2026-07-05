namespace InvoiceProcessor.Configuration;

public sealed class PaymentGatewayOptions
{
    public const string SectionName ="PaymentGateway";
    public string BaseUrl{get; init;} = string.Empty;
}