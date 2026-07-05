using System.Text.Json;
using InvoiceProcessor.Interfaces;
using InvoiceProcessor.Models;

namespace InvoiceProcessor.Repositories;

public sealed class JsonInvoiceRepository : IInvoiceRepository
{
    private const string FilePath = "Fixtures/invoices.json";

    public async Task<IReadOnlyList<Invoice>> LoadInvoicesAsync(
     CancellationToken cancellationToken = default)
    {
        if (!File.Exists(FilePath))
        {
            throw new FileNotFoundException(
                $"Invoice fixture not found at '{FilePath}'.");
        }

        await using var stream = File.OpenRead(FilePath);
        var json = await File.ReadAllTextAsync(FilePath, cancellationToken);

        var options = new JsonSerializerOptions
{
    PropertyNameCaseInsensitive = true
};

var invoices = JsonSerializer.Deserialize<List<Invoice>>(json, options);

        if (invoices is null)
        {
            return [];
        }

        foreach (var invoice in invoices)
        {
            Console.WriteLine(
                $"{invoice.Id} | {invoice.CustomerName} | {invoice.Amount}");
        }

        ValidateInvoices(invoices);

        return invoices;
    }

    private static void ValidateInvoices(IEnumerable<Invoice> invoices)
    {
        var duplicateIds = invoices
            .GroupBy(i => i.Id)
            .Where(g => g.Count() > 1)
            .Select(g => g.Key);

        if (duplicateIds.Any())
        {
            throw new InvalidDataException(
                $"Duplicate invoice IDs found: {string.Join(", ", duplicateIds)}");
        }

        foreach (var invoice in invoices)
        {
            if (invoice.Amount <= 0)
            {
                throw new InvalidDataException(
                    $"Invoice {invoice.Id} has an invalid amount.");
            }

            if (string.IsNullOrWhiteSpace(invoice.CustomerName))
            {
                throw new InvalidDataException(
                    $"Invoice {invoice.Id} has no customer name.");
            }
        }
    }


}