using MockPaymentGateway.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddPaymentSimulation(builder.Configuration);

var app = builder.Build();

var logger = app.Logger;

logger.LogInformation("Mock Payment Gateway started.");

app.MapPaymentEndpoints();

app.Run();