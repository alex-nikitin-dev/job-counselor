// ---------------------------------------------------------------------------
// Entry point for the JobCounselor minimal Web API.
// This file configures the HTTP pipeline and defines all API endpoints.
// ---------------------------------------------------------------------------
using System.Reflection;
using AspNet.Security.OAuth.GitHub;
using AspNet.Security.OAuth.LinkedIn;
using FluentValidation;
using JobCounselor.Application.Commands.CreateProfile;
using JobCounselor.Application.Commands.UpdateProfile;
using JobCounselor.Application.Interfaces;
using JobCounselor.Infrastructure;
using JobCounselor.Infrastructure.Data;
using MediatR;
using Swashbuckle.AspNetCore.Annotations; // for EnableAnnotations extension
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;

// Build the WebApplication using the minimal API approach.
var builder = WebApplication.CreateBuilder(args);

// Enable minimal API endpoint discovery and Swagger generation.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    // Basic API information displayed in Swagger UI/ReDoc
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "JobCounselor API",
        Version = "v1",
        Description = "Minimal API surface for the JobCounselor backend"
    });

    // Allow [SwaggerOperation] and similar attributes.
    options.EnableAnnotations();
});

// ---------------------------------------------------------------------------
// Authentication configuration
// ---------------------------------------------------------------------------

// Register multiple authentication schemes (JWT, cookie and OAuth providers).
builder.Services.AddAuthentication(options =>
{
    // JWT bearer tokens are used by default for API requests.
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    // NOTE: In development we disable all token validation. Replace this with
    // proper signing key and validation settings in production.
    options.TokenValidationParameters = new()
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = false,
        ValidateIssuerSigningKey = false
    };
})
.AddCookie(CookieAuthenticationDefaults.AuthenticationScheme)
.AddGoogle(options =>
{
    // Developer placeholders - replace with real Google OAuth keys.
    options.ClientId = "DEV-GOOGLE-CLIENT-ID";
    options.ClientSecret = "DEV-GOOGLE-CLIENT-SECRET";
})
.AddGitHub(options =>
{
    // Developer placeholders - replace with real GitHub OAuth keys.
    options.ClientId = "DEV-GITHUB-CLIENT-ID";
    options.ClientSecret = "DEV-GITHUB-CLIENT-SECRET";
})
.AddLinkedIn(options =>
{
    // Developer placeholders - replace with real LinkedIn OAuth keys.
    options.ClientId = "DEV-LINKEDIN-CLIENT-ID";
    options.ClientSecret = "DEV-LINKEDIN-CLIENT-SECRET";
});

// Authorization services are required for the [Authorize] attribute and
// authorization middleware.
builder.Services.AddAuthorization();

// ---------------------------------------------------------------------------
// Dependency injection setup
// ---------------------------------------------------------------------------

// Assembly containing the Application layer types used for scanning.
var applicationAssembly = typeof(CreateProfileCommand).Assembly;

// Register MediatR handlers and FluentValidation validators discovered in the
// Application project assembly.
// MediatR v11 only supports registering assemblies directly. This scans
// the Application project for request handlers and related services.
builder.Services.AddMediatR(applicationAssembly);
builder.Services.AddValidatorsFromAssembly(applicationAssembly);

// Register infrastructure services (EF Core, repositories, logging, etc.).
builder.Services.AddInfrastructure(builder.Configuration, builder.Environment);

var app = builder.Build();

// ---------------------------------------------------------------------------
// Middleware pipeline
// ---------------------------------------------------------------------------

// Enable authentication/authorization for incoming requests.
app.UseAuthentication();
app.UseAuthorization();

// Serve the OpenAPI specification at /openapi.yaml.
app.UseSwagger(options =>
{
    options.RouteTemplate = "openapi.yaml";
});

// Provide the classic Swagger UI at /swagger and the ReDoc UI at /redoc.
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/openapi.yaml", "JobCounselor API V1");
});
app.UseReDoc(options =>
{
    options.RoutePrefix = "redoc";        // ReDoc available at /redoc
    options.SpecUrl = "/openapi.yaml";   // Points to the Swagger YAML document
});

// ---------------------------------------------------------------------------
// API endpoint definitions
// ---------------------------------------------------------------------------

// All endpoints live under /api/v1 and require authentication.
var api = app.MapGroup("/api/v1").RequireAuthorization();

// ---------------------------- Profile endpoints ----------------------------

// Listing all profiles is not yet implemented in the repository layer.
// For now return a placeholder response.
api.MapGet("/profiles", () => Results.Ok("profile list placeholder"))
   .WithName("GetProfiles")
   .WithTags("Profile")
   .WithOpenApi();

api.MapPost("/profiles", async (CreateProfileCommand command, IMediator mediator) =>
        await mediator.Send(command))
   .WithName("CreateProfile")
   .WithTags("Profile")
   .WithOpenApi();

api.MapPut("/profiles/{id:guid}", async (
        Guid id,
        UpdateProfileCommand command,
        IMediator mediator) =>
    {
        // Pass the route identifier through the command record.
        var request = command with { ProfileId = id };
        return await mediator.Send(request);
    })
   .WithName("UpdateProfile")
   .WithTags("Profile")
   .WithOpenApi();

// ----------------------------- Resume endpoints ----------------------------

api.MapGet("/resumes", () => Results.Ok("resume list placeholder"))
   .WithName("GetResumes")
   .WithTags("Resume")
   .WithOpenApi();

// --------------------------- Cover letter endpoints -----------------------

api.MapGet("/coverletters", () => Results.Ok("cover letter list placeholder"))
   .WithName("GetCoverLetters")
   .WithTags("CoverLetter")
   .WithOpenApi();

// ------------------------------ Job endpoints ------------------------------

api.MapGet("/jobs", () => Results.Ok("job list placeholder"))
   .WithName("GetJobs")
   .WithTags("Job")
   .WithOpenApi();

// --------------------------- Analytics endpoints --------------------------

api.MapGet("/analytics", () => Results.Ok("analytics placeholder"))
   .WithName("GetAnalytics")
   .WithTags("Analytics")
   .WithOpenApi();

// Seed development data and start the application.
if (app.Environment.IsDevelopment())
{
    using var scope = app.Services.CreateScope();
    var seeder = scope.ServiceProvider.GetRequiredService<DbSeeder>();
    await seeder.SeedAsync();
}

await app.RunAsync();

