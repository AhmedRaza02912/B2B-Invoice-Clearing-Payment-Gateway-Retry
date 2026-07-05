using System.Net;
using System.Net.Http.Json;
using Microsoft.Extensions.Options;
using InvoiceProcessor.Configuration;
using InvoiceProcessor.Interfaces;
using InvoiceProcessor.Models;

namespace InvoiceProcessor.Services;

public sealed class PaymentGateway : IPaymentGateway
{
    private readonly HttpClient _httpClient;

    public PaymentGateway(
        HttpClient httpClient,
        IOptions<PaymentGatewayOptions> options
    )
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri(options.Value.BaseUrl);

    }

    public async Task<PaymentResponse> ProcessPaymentAsync(
        Invoice invoice, CancellationToken cancellationToken = default
    )
    {
        var response = await _httpClient.PostAsJsonAsync(
            "/pay",
            new
            {
                invoice.Id,
                invoice.Amount
            },
            cancellationToken
        );
        return response.StatusCode switch
        {
            HttpStatusCode.OK => new PaymentResponse
            {
                Result = PaymentResult.Success,
                Message = "Payment Successful"
            },
            HttpStatusCode.TooManyRequests => new PaymentResponse
            {
                Result = PaymentResult.RateLimited,
                Message = "Rate Limited."
            },
            HttpStatusCode.InternalServerError => new PaymentResponse
            {
              Result = PaymentResult.InternalServerError,
              Message = "Server Error."  
            },
            _ => throw new HttpRequestException(
                $"Unexpected Status Code: {response.StatusCode}"
            )
        };
    }
}