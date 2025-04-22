using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace MultiEnvSwagger
{
    public class ProdOnlyFilter : IDocumentFilter
    {
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            var allowedPaths = swaggerDoc.Paths
                .Where(p => p.Key.Contains("Prod"))
                .ToDictionary(p => p.Key, p => p.Value);

            swaggerDoc.Paths = new OpenApiPaths();
            foreach (var path in allowedPaths)
            {
                swaggerDoc.Paths.Add(path.Key, path.Value);
            }
        }
    }
}
