using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Diaverum.API.SwaggerConfig
{
    public class ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider)
        : IConfigureNamedOptions<SwaggerGenOptions>
    {
        public void Configure(SwaggerGenOptions options)
        {
            // add swagger document for every API version discovered
            foreach (var description in provider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(
                    description.GroupName,
                    CreateVersionInfo(description));
            }
        }

        public void Configure(string? name, SwaggerGenOptions options)
        {
            Configure(options);
        }

        private static OpenApiInfo CreateVersionInfo(ApiVersionDescription desc)
        {
            var info = new OpenApiInfo()
            {
                //Version = $"{typeof(Program).GetReleaseVersion()}",
                Title = "Diaverum API",
                Description = @"Backend for Diaverum to fetch data. This backend provides 
                                endpoints for all necessary features.",
                Contact = new OpenApiContact
                {
                    Name = "Citou AB",
                    Email = "Rikard Gustafsson <rikard.gustafsson@citou.se>"
                },
                License = new OpenApiLicense
                {
                    Name = "Use under GPL",
                }
            };

            if (desc.IsDeprecated)
            {
                info.Description += " This API version has been deprecated. Please use one of the new APIs available from the explorer.";
            }

            return info;
        }
    }
}
