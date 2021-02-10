using System;
using System.IO;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Accounts.API.Infrastructure.Swagger
{
    public static class ServiceCollectionExtensions
    {

        public static IServiceCollection AddCustomSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", CreateOpenApiInfo());
                options.IncludeXmlComments(GetXmlCommentsPath());
            });
            return services;
        }

        private static OpenApiInfo CreateOpenApiInfo()
        {
            return new OpenApiInfo
            {
                Title = "Accounts API",
                Version = "v1",
                Description = "Manage user personal finance accounts.",
                Contact = new OpenApiContact
                {
                    Name = "Caio Cavalcanti",
                    Email = "caiofabiomc@gmail.com",
                    Url = new Uri("https://github.com/CaioCavalcanti")
                },
            };
        }

        private static string GetXmlCommentsPath()
        {
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            return Path.Combine(AppContext.BaseDirectory, xmlFile);
        }
    }
}