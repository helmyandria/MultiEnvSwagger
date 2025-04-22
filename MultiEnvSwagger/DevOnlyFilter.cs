using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace MultiEnvSwagger
{
    public class DevOnlyFilter : IDocumentFilter
    {
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            // Hapus semua path kecuali yang Dev-only
            var allowedPaths = swaggerDoc.Paths
                .Where(p => p.Key.Contains("Dev"))
                .ToDictionary(p => p.Key, p => p.Value);

            swaggerDoc.Paths = new OpenApiPaths();
            foreach (var path in allowedPaths)
            {
                swaggerDoc.Paths.Add(path.Key, path.Value);
            }
        }
    }
}
