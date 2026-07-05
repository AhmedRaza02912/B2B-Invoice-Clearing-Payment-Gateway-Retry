using InvoiceProcessor.Configuration;
using InvoiceProcessor.Interfaces;
using InvoiceProcessor.Repositories;
using InvoiceProcessor.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

var builder = Host.CreateApplicationBuilder(args);

// Logging
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

// Register EVERYTHING here
builder.Services.Configure<RetryOptions>(
    builder.Configuration.GetSection(RetryOptions.SectionName));

builder.Services.Configure<PaymentGatewayOptions>(
    builder.Configuration.GetSection(PaymentGatewayOptions.SectionName));

builder.Services.AddSingleton<IInvoiceRepository, JsonInvoiceRepository>();

builder.Services.AddHttpClient<IPaymentGateway, PaymentGateway>();

builder.Services.AddHostedService<InvoiceProcessingService>();

// ONLY NOW build the host
var app = builder.Build();

await app.RunAsync();