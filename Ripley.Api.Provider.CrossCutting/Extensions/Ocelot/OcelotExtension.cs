using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Ripley.Api.Provider.CrossCutting.Extensions.Ocelot
{
    public static class OcelotExtension
    {
        public class HideOcelotFilterExtension : IDocumentFilter
        {
            private static readonly string[] _ignoredPaths = { "/configuration", "/outputcache/{region}" };

            public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
            {
                foreach (var ignorePath in _ignoredPaths)
                {
                    swaggerDoc.Paths.Remove(ignorePath);
                }
            }
        }
    }
}
