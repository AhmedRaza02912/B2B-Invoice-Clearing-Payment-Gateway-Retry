using InvoiceProcessor.Models;

namespace InvoiceProcessor.Interfaces;

// Sends invoice payment to external payment gateway.

public interface IPaymentGateway
{
    // Cancellation token allows cancelling async tasks if app crahses.
    Task<PaymentResponse> ProcessPaymentAsync(
        Invoice invoice,
        CancellationToken cancellationToken = default
    );
}