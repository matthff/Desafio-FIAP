using System;
using System.Text;
using Api.Domain.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;


namespace Api.CrossCutting.Dependecies;

public static class ConfigureAuthentication
{
    public static IServiceCollection AddTokenConfiguration(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var tokenConfigurations = new TokenConfiguration();
        configuration.GetSection("TokenConfigurations").Bind(tokenConfigurations);

        services.AddSingleton(tokenConfigurations);

        return services;
    }

    public static IServiceCollection AddJwtAuthentication(
        this IServiceCollection services)
    {
        var serviceProvider = services.BuildServiceProvider();
        var tokenConfigurations = serviceProvider.GetRequiredService<TokenConfiguration>();

        services.AddAuthentication(options =>
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
                ValidIssuer = tokenConfigurations.Issuer,
                ValidAudience = tokenConfigurations.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(tokenConfigurations.Secret)
                ),
                ClockSkew = TimeSpan.Zero
            };
        });



        return services;
    }

    public static IServiceCollection AddAuthorizationPolicies(this IServiceCollection services)
    {
        services.AddAuthorizationBuilder()
            .AddPolicy("Bearer", new AuthorizationPolicyBuilder()
            .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
            .RequireAuthenticatedUser()
            .Build());

        return services;
    }
}
