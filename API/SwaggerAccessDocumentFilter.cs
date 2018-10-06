using System;
using System.Security.Principal;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace API
{
    public class SwaggerAccessDocumentFilter : IDocumentFilter
    {
        private readonly IHttpContextAccessor _serviceProvider;

        public SwaggerAccessDocumentFilter(IHttpContextAccessor serviceProvider)
        {
            _serviceProvider = serviceProvider ?? throw new ArgumentException(nameof(serviceProvider));
        }

        public void Apply(
            SwaggerDocument swaggerDoc,
            DocumentFilterContext context)
        {
            var principal = _serviceProvider;

            if (!principal.HttpContext.User.Identity.IsAuthenticated)
            {
                swaggerDoc.Paths.Clear();
                swaggerDoc.Definitions.Clear();
            }
        }
    }
}