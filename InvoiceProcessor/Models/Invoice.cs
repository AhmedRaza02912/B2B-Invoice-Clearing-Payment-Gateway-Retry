using System.Text.Json.Serialization;

namespace InvoiceProcessor.Models;

public sealed class Invoice
{
    [JsonPropertyName("id")]
    public Guid Id { get; init; }

    [JsonPropertyName("customerName")]
    public string CustomerName { get; init; } = string.Empty;

    [JsonPropertyName("amount")]
    public decimal Amount { get; init; }

    public InvoiceStatus Status { get; set; } = InvoiceStatus.Pending;
}