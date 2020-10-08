using System;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using NSwag;
using NSwag.Generation.Processors.Security;

namespace DocumentsSynchronizer.Extension
{
    public static class SwaggerExtension
    {
        /// <summary>
        /// </summary>
        /// <param name="services"></param>
        /// <param name="title"></param>
        /// <param name="description"></param>
        public static void AddSwaggerDocumentWithConfiguration(this IServiceCollection services, string title,
            string description)
        {
            services.AddSwaggerDocument(config =>
            {
                // config.DocumentName = "1.0";
                //  config.ApiGroupNames = new[] { "1.0-beta" };
                config.RequireParametersWithoutDefault = false;

                config.DocumentProcessors.Add(
#pragma warning disable 618
                    new SecurityDefinitionAppender("jwtToken", new OpenApiSecurityScheme
#pragma warning restore 618
                    {
                        Description = "Copy 'Bearer ' + valid JWT token into field",
                        Type = OpenApiSecuritySchemeType.ApiKey,
                        Scheme = "bearer",
                        Name = "jwtToken",
                        In = OpenApiSecurityApiKeyLocation.Header
                    }));

                config.OperationProcessors.Add(new OperationSecurityScopeProcessor("jwtToken"));

                config.Title = title;
                config.Description = description;
                //    config.Version = "1.0.0";
                config.PostProcess = document =>
                {
                    document.Info.Contact = new OpenApiContact
                    {
                        Name = $"M.Salari @ {DateTime.Now.Year}",
                        Email = "mohammad.s.salari(at)gmail.com",
                        Url = ""
                    };
                };
            });
        }

        /// <summary>
        /// </summary>
        /// <param name="app"></param>
        /// <param name="virtualPath"></param>
        public static void UseSwaggerWithConfiguration(this IApplicationBuilder app, string virtualPath = null)
        {
            app.UseOpenApi(config =>
            {
                config.PostProcess = (document, request) =>
                {
                    if (request.Headers.ContainsKey("X-External-Host"))
                    {
                        document.Host = request.Headers["X-External-Host"].First();
                        document.BasePath = request.Headers["X-External-Path"].First();
                    }
                };
            });

            app.UseSwaggerUi3(config =>
            {
                config.TransformToExternalPath = (internalUiRoute, request) =>
                {
                    // The header X-External-Path is set in the nginx.conf file
                    if (internalUiRoute.StartsWith("/") && internalUiRoute.StartsWith(request.PathBase) == false)
                        return request.PathBase + internalUiRoute;
                    return internalUiRoute;
                };
            });
        }
    }
}