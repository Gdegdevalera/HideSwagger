using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace API
{
    public static class SwaggerExtensions
    {
        public static void ConfigureSwagger(this IServiceCollection services, string autority)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" });

                c.AddSecurityDefinition("oauth2", new OAuth2Scheme
                {
                    Flow = Constants.Flow,
                    TokenUrl = autority + "/connect/token",
                    Scopes = new Dictionary<string, string>
                    {
                        { Constants.Scope, "Custom scope" }
                    }
                });

                c.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>>
                {
                    { "oauth2", new[] { Constants.Scope } }
                });
            });
        }

        public static void UseSwaggerUi(this IApplicationBuilder app)
        {
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");

                c.OAuthClientId(Constants.ClientId);
                c.OAuthAppName("Demo API - Swagger");
                c.OAuthClientSecret(Constants.ClientSecret);
            });
        }
    }
}