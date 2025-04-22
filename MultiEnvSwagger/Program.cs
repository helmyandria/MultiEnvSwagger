
using Microsoft.OpenApi.Models;

namespace MultiEnvSwagger
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();

            // Ambil environment dari variable eksternal
            var swaggerEnv = Environment.GetEnvironmentVariable("SWAGGER_ENV") ?? "dev";

            // Swagger Gen setup
            builder.Services.AddSwaggerGen(options =>
            {
                // Register dua dokumen
                options.SwaggerDoc("dev", new OpenApiInfo { Title = "Dev API", Version = "v1" });
                options.SwaggerDoc("prod", new OpenApiInfo { Title = "Prod API", Version = "v1" });

                // Filter endpoint berdasarkan DocName dan namespace controller
                options.DocInclusionPredicate((docName, apiDesc) =>
                {
                    var groupName = apiDesc.GroupName?.ToLower();

                    return docName switch
                    {
                        "dev" => groupName == "dev",
                        "prod" => groupName == "prod",
                        _ => false
                    };
                });

                // tag controller ke GroupName
                options.TagActionsBy(api => new[] { api.GroupName ?? "default" });
            });

            var app = builder.Build();

            app.UseSwagger();
            
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/dev/swagger.json", "MultiEnvSwagger API - Dev");
                c.SwaggerEndpoint("/swagger/prod/swagger.json", "MultiEnvSwagger API - Prod");

                c.RoutePrefix = "swagger";
            });

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
