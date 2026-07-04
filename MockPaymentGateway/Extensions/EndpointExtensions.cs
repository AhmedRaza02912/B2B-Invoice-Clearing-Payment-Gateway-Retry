using Microsoft.AspNetCore.Http.HttpResults;
using MockPaymentGateway.Contracts;
using MockPaymentGateway.Models;
using MockPaymentGateway.Services;

namespace MockPaymentGateway.Extensions;

public static class EndpointExtensions
{
    public static IEndpointRouteBuilder MapPaymentEndpoints(
        this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapPost("/pay",
            (PaymentRequest request,
             IPaymentSimulationService simulator) =>
            {
                var result = simulator.ProcessPayment(request);

                return result.Outcome switch
                {
                    PaymentOutcome.Success =>
                        Results.Ok(result),

                    PaymentOutcome.InternalServerError =>
                        Results.Json(
                            result,
                            statusCode: StatusCodes.Status500InternalServerError),

                    PaymentOutcome.RateLimited =>
                        Results.Json(
                            result,
                            statusCode: StatusCodes.Status429TooManyRequests),

                    _ =>
                        Results.Json(
                            result,
                            statusCode: StatusCodes.Status500InternalServerError)
                };
            });

        return endpoints;
    }
}