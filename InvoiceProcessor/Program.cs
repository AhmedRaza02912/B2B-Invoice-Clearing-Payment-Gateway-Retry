using InvoiceProcessor.Configuration;
using InvoiceProcessor.Interfaces;
using InvoiceProcessor.Repositories;
using InvoiceProcessor.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

var builder = Host.CreateApplicationBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

builder.Services.Configure<RetryOptions>(
    builder.Configuration.GetSection(RetryOptions.SectionName));

builder.Services.Configure<PaymentGatewayOptions>(
    builder.Configuration.GetSection(PaymentGatewayOptions.SectionName));

builder.Services.AddSingleton<IInvoiceRepository, JsonInvoiceRepository>();
builder.Services.AddScoped<IInvoiceProcessor, InvoiceRetryProcessor>();
builder.Services.AddHttpClient<IPaymentGateway, PaymentGateway>();
builder.Services.AddSingleton<IRetryPolicy, ExponentialRetryPolicy>();

builder.Services.AddHostedService<InvoiceProcessingService>();

var app = builder.Build();

await app.RunAsync();