namespace MockPaymentGateway.Configuration;

// Control how often a response type is returned

public sealed class PaymentSimulationOptions
{
    public const string SectionName = "PaymentSimulation";
    public int SuccessRate{get; init;}
    public int InternalServerErrorRate{get; init;}
    public int RateLimitRate{get; init;}
    
}