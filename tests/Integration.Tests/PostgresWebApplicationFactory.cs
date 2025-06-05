using System.Linq;
using Microsoft.AspNetCore.Authentication;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using JobCounselor.Infrastructure.Data;
using JobCounselor.Application.Interfaces;
using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Containers;
using DotNet.Testcontainers.Configurations;

namespace JobCounselor.Integration.Tests;

/// <summary>
/// Custom <see cref="WebApplicationFactory{TEntryPoint}"/> that spins up a
/// PostgreSQL container and configures the API to use it during tests.
/// </summary>
public class PostgresWebApplicationFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    private readonly PostgreSqlTestcontainer _postgres;

    public PostgresWebApplicationFactory()
    {
        _postgres = new TestcontainersBuilder<PostgreSqlTestcontainer>()
            .WithDatabase(new PostgreSqlTestcontainerConfiguration
            {
                Database = "jobdb",
                Username = "postgres",
                Password = "postgres"
            })
            .Build();
    }

    /// <inheritdoc />
    public async Task InitializeAsync()
    {
        if (!File.Exists("/usr/bin/docker"))
        {
            throw new Xunit.Sdk.SkipException("Docker is not available");
        }

        await _postgres.StartAsync();
    }

    /// <inheritdoc />
    public new async Task DisposeAsync() => await _postgres.DisposeAsync();

    /// <summary>
    /// Reconfigure the application services to use PostgreSQL and the test
    /// authentication scheme.
    /// </summary>
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            // Replace the DbContext registered by the API with one pointing
            // at the PostgreSQL container.
            var descriptor = services.Single(d => d.ServiceType == typeof(DbContextOptions<AppDbContext>));
            services.Remove(descriptor);

            services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(_postgres.ConnectionString));

            // Register simple stub services used by the application command handlers.
            services.AddTransient<IResumeService, StubResumeService>();
            services.AddTransient<ICoverLetterService, StubCoverLetterService>();

            // Switch authentication to the lightweight test scheme.
            services.AddAuthentication(TestAuthHandler.SchemeName)
                .AddScheme<AuthenticationSchemeOptions, TestAuthHandler>(TestAuthHandler.SchemeName, _ => { });
        });
    }
}
