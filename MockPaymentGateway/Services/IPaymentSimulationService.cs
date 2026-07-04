using MockPaymentGateway.Contracts;
using MockPaymentGateway.Models;

namespace MockPaymentGateway.Services;

public interface IPaymentSimulationService
{
    PaymentResult ProcessPayment(PaymentRequest request);
}