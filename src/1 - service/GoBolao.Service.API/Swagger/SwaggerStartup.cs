using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace GoBolao.Service.API.Swagger
{
    public class SwaggerStartup
    {
        public static void ServicoSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "API GoBolao",
                    Description = "Versão 1 da API da aplicação GoBolao"
                });

                c.SwaggerDoc("v2", new OpenApiInfo
                {
                    Version = "v2",
                    Title = "API Doczen",
                    Description = "Versão 2 da API da aplicação GoBolao"
                });

                c.AddSecurityDefinition("bearer", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Scheme = "bearer",
                    Description = "Preencha o seu token para autenticar."
                });


                c.DocInclusionPredicate((version, desc) =>
                {

                    if (!desc.TryGetMethodInfo(out MethodInfo methodInfo)) return false;
                    var versions = methodInfo.DeclaringType
                        .GetCustomAttributes(true)
                        .OfType<ApiVersionAttribute>()
                        .SelectMany(attr => attr.Versions);


                    var maps = methodInfo
                        .GetCustomAttributes(true)
                        .OfType<MapToApiVersionAttribute>()
                        .SelectMany(attr => attr.Versions)
                        .ToArray();

                    return versions.Any(v => $"v{v}" == version)
                           && (!maps.Any() || maps.Any(v => $"v{v}" == version));
                });

                c.OperationFilter<FiltroRequisicoesAutenticacao>();
                c.OperationFilter<RemoveVersionParameterFilter>();
                c.DocumentFilter<ReplaceVersionWithExactValueInPathFilter>();
                var arquivoXML = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var caminhoXML = Path.Combine(AppContext.BaseDirectory, arquivoXML);
                c.IncludeXmlComments(caminhoXML);
                //c.CustomSchemaIds(s => s.FullName);
            });
        }

        public static void AplicacaoSwagger(IApplicationBuilder app)
        {
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Versão API v1");
                c.SwaggerEndpoint("/swagger/v2/swagger.json", "Versão API v2");
                //c.RoutePrefix = string.Empty;
            });
        }

        public class RemoveVersionParameterFilter : IOperationFilter
        {
            public void Apply(OpenApiOperation operation, OperationFilterContext context)
            {
                if (!operation.Parameters.Any())
                    return;

                var versionParameter = operation.Parameters
                    .FirstOrDefault(p => p.Name.ToLower() == "version");

                if (versionParameter != null)
                    operation.Parameters.Remove(versionParameter);
            }
        }

        public class ReplaceVersionWithExactValueInPathFilter : IDocumentFilter
        {
            public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
            {

                if (swaggerDoc == null)
                    throw new ArgumentNullException(nameof(swaggerDoc));

                var replacements = new OpenApiPaths();

                foreach (var (key, value) in swaggerDoc.Paths)
                {
                    replacements.Add(key.Replace("v{version}", swaggerDoc.Info.Version,
                            StringComparison.InvariantCulture), value);
                }

                swaggerDoc.Paths = replacements;
            }
        }
    }
}
