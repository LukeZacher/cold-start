using ColdStart.Api.Options;

namespace ColdStart.Api.Extensions;

public static class OptionServiceCollectionExtensions
{
    public static IServiceCollection AddValidatedOptions<T>(
        this IServiceCollection services,
        IConfiguration configuration,
        string sectionName
    )
        where T : class
    {
        services
            .AddOptions<T>()
            .Bind(configuration.GetSection(sectionName))
            .ValidateDataAnnotations()
            .ValidateOnStart();

        return services;
    }

    public static IServiceCollection AddColdStartOptions(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.AddValidatedOptions<AuthOptions>(configuration, "Auth");
        services.AddValidatedOptions<JwtOptions>(configuration, "Jwt");
        services.AddValidatedOptions<ResendOptions>(configuration, "Resend");
        services.AddValidatedOptions<IdentityPolicyOptions>(configuration, "IdentityPolicy");

        return services;
    }
}
