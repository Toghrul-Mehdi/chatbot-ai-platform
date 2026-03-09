using ChatBot.Application.Interfaces;
using ChatBot.Infrastructure.ExternalServices;
using ChatBot.Infrastructure.Repositories;
using ChatBot.Shared.Constants;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Extensions.Http;

namespace ChatBot.Infrastructure;

/// <summary>
/// Dependency injection extensions for infrastructure services.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Registers infrastructure services with the DI container.
    /// </summary>
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        // Repositories (singleton for in-memory)
        services.AddSingleton<IUserRepository, UserRepository>();
        services.AddSingleton<IChatSessionRepository, ChatSessionRepository>();

        // AI Service HTTP client with Polly resilience
        var aiBaseUrl = configuration["AiServiceSettings:BaseUrl"] ?? "http://localhost:8000";
        var timeoutSeconds = configuration.GetValue<int>("AiServiceSettings:TimeoutSeconds", 30);
        var retryCount = configuration.GetValue<int>("AiServiceSettings:RetryCount", 3);

        services.AddHttpClient(AppConstants.AiServiceHttpClient, client =>
        {
            client.BaseAddress = new Uri(aiBaseUrl);
            client.Timeout = TimeSpan.FromSeconds(timeoutSeconds);
        })
        .AddPolicyHandler(GetRetryPolicy(retryCount))
        .AddPolicyHandler(GetCircuitBreakerPolicy());

        services.AddScoped<IAiServiceClient, AiServiceClient>();

        return services;
    }

    private static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy(int retryCount)
    {
        return HttpPolicyExtensions
            .HandleTransientHttpError()
            .WaitAndRetryAsync(retryCount, retryAttempt =>
                TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
    }

    private static IAsyncPolicy<HttpResponseMessage> GetCircuitBreakerPolicy()
    {
        return HttpPolicyExtensions
            .HandleTransientHttpError()
            .CircuitBreakerAsync(5, TimeSpan.FromSeconds(30));
    }
}
