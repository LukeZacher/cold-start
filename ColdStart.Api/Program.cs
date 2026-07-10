using System.Text;
using ColdStart.Api.Data;
using ColdStart.Api.Models.Entities;
using ColdStart.Api.Options;
using ColdStart.Api.Repositories;
using ColdStart.Api.Repositories.Interfaces;
using ColdStart.Api.Services;
using ColdStart.Api.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Resend;
using Serilog;
using ILogger = Microsoft.Extensions.Logging.ILogger;

// Bootstrap logger for startup
Serilog.Log.Logger = new LoggerConfiguration()
    .WriteTo.Console(theme: Serilog.Sinks.SystemConsole.Themes.AnsiConsoleTheme.Code)
    .CreateBootstrapLogger();

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog(
    (context, services, configuration) =>
    {
        configuration
            .ReadFrom.Configuration(context.Configuration)
            .ReadFrom.Services(services)
            .WriteTo.Console(theme: Serilog.Sinks.SystemConsole.Themes.AnsiConsoleTheme.Code);
    }
);

// Add services to the container.

builder.Services.AddControllers(options =>
{
    options.Filters.Add(new AuthorizeFilter());
});

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

builder.Services.AddCors(options =>
{
    options.AddPolicy(
        "LocalDev",
        policy =>
        {
            policy.WithOrigins("http://localhost:4200").AllowAnyHeader().AllowAnyMethod();
        }
    );
});

IdentityPolicyOptions identityPolicyOptions = builder
    .Configuration.GetSection("IdentityPolicy")
    .Get<IdentityPolicyOptions>()!;

builder
    .Services.AddIdentity<User, IdentityRole>(options =>
    {
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireUppercase = false;
        options.Password.RequireDigit = false;
        options.Password.RequiredLength = identityPolicyOptions.PasswordRequiredLength;
        options.User.RequireUniqueEmail = true;
        options.Lockout.MaxFailedAccessAttempts = identityPolicyOptions.MaxFailedAccessAttempts;
        options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(
            identityPolicyOptions.LockoutMinutes
        );
        options.SignIn.RequireConfirmedEmail = true;
    })
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

// --- Options pattern: DI registration + startup validation for all config-bound options ---
builder
    .Services.AddOptions<AuthOptions>()
    .Bind(builder.Configuration.GetSection("Auth"))
    .ValidateDataAnnotations()
    .ValidateOnStart();

builder
    .Services.AddOptions<JwtOptions>()
    .Bind(builder.Configuration.GetSection("Jwt"))
    .ValidateDataAnnotations()
    .ValidateOnStart();

builder
    .Services.AddOptions<ResendOptions>()
    .Bind(builder.Configuration.GetSection("Resend"))
    .ValidateDataAnnotations()
    .ValidateOnStart();

builder
    .Services.AddOptions<IdentityPolicyOptions>()
    .Bind(builder.Configuration.GetSection("IdentityPolicy"))
    .ValidateDataAnnotations()
    .ValidateOnStart();

AuthOptions authOptions = builder.Configuration.GetSection("Auth").Get<AuthOptions>()!;

builder.Services.Configure<DataProtectionTokenProviderOptions>(options =>
{
    options.TokenLifespan = TimeSpan.FromHours(authOptions.TokenLifespanHours);
});

JwtOptions jwtOptions = builder.Configuration.GetSection("Jwt").Get<JwtOptions>()!;

builder
    .Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtOptions.Issuer,
            ValidAudience = jwtOptions.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Key)),
        };
    });

//builder.Services.AddOptions();

ResendOptions resendOptions = builder.Configuration.GetSection("Resend").Get<ResendOptions>()!;

builder.Services.AddResend(options =>
{
    options.ApiToken = resendOptions.ApiKey;
});

builder.Services.AddScoped<IBrandRepository, BrandRepository>();
builder.Services.AddScoped<IBrandService, BrandService>();
builder.Services.AddScoped<ICompanyRepository, CompanyRepository>();
builder.Services.AddScoped<ICompanyService, CompanyService>();
builder.Services.AddScoped<ICountryRepository, CountryRepository>();
builder.Services.AddScoped<ICountryService, CountryService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IModelRepository, ModelRepository>();
builder.Services.AddScoped<IModelService, ModelService>();

var app = builder.Build();

app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        IExceptionHandlerFeature? exceptionFeature =
            context.Features.Get<IExceptionHandlerFeature>();
        ILogger logger = context.RequestServices.GetRequiredService<ILogger<Program>>();

        context.Response.StatusCode = 500;
        context.Response.ContentType = "application/json";

        if (exceptionFeature?.Error != null)
        {
            logger.LogError(
                exceptionFeature.Error,
                "Unhandled exception: {Message}",
                exceptionFeature.Error.Message
            );

            await context.Response.WriteAsJsonAsync(
                app.Environment.IsDevelopment()
                    ? new { message = exceptionFeature.Error.Message }
                    : new { message = "An unexpected error occurred. Please try again later." }
            );
        }
        else
        {
            await context.Response.WriteAsJsonAsync(
                new { message = "An unexpected error occurred. Please try again later." }
            );
        }
    });
});

app.UseSerilogRequestLogging();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseCors("LocalDev");

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
