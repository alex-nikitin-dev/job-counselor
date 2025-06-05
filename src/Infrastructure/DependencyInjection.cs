using System.IO;
using JobCounselor.Application.Interfaces;
using JobCounselor.Infrastructure.Data;
using JobCounselor.Infrastructure.Repositories;
using JobCounselor.Infrastructure.Services;
using JobCounselor.Domain.Entities;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Serilog;

namespace JobCounselor.Infrastructure;

/// <summary>
/// Extension methods for registering infrastructure services in the
/// dependency injection container.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Adds infrastructure services to the provided <see cref="IServiceCollection"/>.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <param name="configuration">Application configuration.</param>
    /// <param name="environment">Current hosting environment.</param>
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration,
        IHostEnvironment environment)
    {
        // Configure Entity Framework Core using the in-memory provider by default.
        services.AddDbContext<AppDbContext>(options =>
            options.UseInMemoryDatabase("AppDb"));

        // Register repositories.
        services.AddScoped<IRepository<Profile>, EfRepository<Profile>>();
        services.AddScoped<IProfileRepository, ProfileRepository>();

        // Register the AI cover letter provider and associated HttpClient.
        services.AddHttpClient<IAiCoverLetterProvider, OllamaCoverLetterProvider>();

        // Configure data protection key persistence outside of Azure for local development.
        if (string.IsNullOrEmpty(Environment.GetEnvironmentVariable("WEBSITE_INSTANCE_ID")))
        {
            services.AddDataProtection()
                .PersistKeysToFileSystem(new DirectoryInfo("./keys"));
        }
        else
        {
            services.AddDataProtection();
        }

        // Setup Serilog for structured logging to console and rolling file.
        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(configuration)
            .WriteTo.Console(formatter: new Serilog.Formatting.Json.JsonFormatter())
            .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day)
            .CreateLogger();

        services.AddLogging(loggingBuilder => loggingBuilder.AddSerilog());

        // OpenTelemetry tracing and metrics with OTLP exporter.
        services.AddOpenTelemetry()
            .ConfigureResource(res => res.AddService("JobCounselor"))
            .WithTracing(tracer =>
            {
                tracer.AddAspNetCoreInstrumentation();
                tracer.AddEntityFrameworkCoreInstrumentation();
                tracer.AddOtlpExporter(o => o.Endpoint = new Uri("http://tempo:4317"));
            })
            .WithMetrics(metrics =>
            {
                metrics.AddAspNetCoreInstrumentation();
                metrics.AddRuntimeInstrumentation();
                metrics.AddProcessInstrumentation();
                metrics.AddOtlpExporter(o => o.Endpoint = new Uri("http://tempo:4317"));
            });

        return services;
    }
}
