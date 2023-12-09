using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using static Ripley.Api.Provider.CrossCutting.Extensions.Ocelot.OcelotExtension;

namespace Ripley.Api.Provider.CrossCutting.Extensions.Swagger
{
    public static class SwaggerExtesion
    {
        public static void AddSwaggerExtension(this IServiceCollection services, string jwtTitle)
        {
            services.AddSwaggerGen(c =>
            {
                c.DocumentFilter<HideOcelotFilterExtension>();
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = jwtTitle,
                    Version = "1.0"
                });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,
                        },
                        new List<string>()
                    }
                });
            });
        }
    }
}
