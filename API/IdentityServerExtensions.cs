using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityModel;
using IdentityServer4.AccessTokenValidation;
using IdentityServer4.Models;
using IdentityServer4.Test;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace API
{
    public static class IdentityServerExtensions
    {
        public static void ConfigureIdentityServer(this IServiceCollection services)
        {
            var client = new Client
            {
                ClientId = Constants.ClientId,
                ClientName = "Example Client Credentials Client Application",
                AllowedGrantTypes = new [] { Constants.Flow },
                ClientSecrets = new List<Secret>
                {
                    new Secret(Constants.ClientSecret.Sha256())
                },
                AllowedScopes = new List<string> { Constants.Scope }
            };

            var resource = new ApiResource
            {
                Name = Constants.ResourceId,
                DisplayName = "Custom API",
                Description = "Custom API Access",
                UserClaims = new List<string> { JwtClaimTypes.Email, JwtClaimTypes.Role },
                ApiSecrets = new List<Secret> { new Secret(Constants.ResourceSecret.Sha256())},
                Scopes = new List<Scope>
                {
                    new Scope(Constants.Scope)
                }
            };

            var user = new TestUser
            {
                SubjectId = "1",
                Username = "user",
                Password = "pass",
                Claims = new List<Claim>
                {
                    new Claim(JwtClaimTypes.Email, "user@test.com"),
                    new Claim(JwtClaimTypes.Role, "user")
                }
            };

            services.AddIdentityServer()
                .AddInMemoryClients(new [] { client })
                .AddInMemoryIdentityResources(new IdentityResource[] {
                    new IdentityResources.OpenId(),
                    new IdentityResources.Profile()
                })
                .AddInMemoryApiResources(new [] { resource })
                .AddTestUsers(new List<TestUser> { user })
                .AddDeveloperSigningCredential();
        }

        public static void ConfigureAuthorization(this IServiceCollection services, string authority)
        {
            services
                .AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.Authority = authority;
                    options.RequireHttpsMetadata = false;
                    options.Audience = Constants.ResourceId;
                    options.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        {
                            if (context.Request.Query.TryGetValue("token", out var token))
                            {
                                context.Token = token;
                            }

                            return Task.CompletedTask;
                        }
                    };
                });
        }
    }
}