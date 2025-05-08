using Microsoft.OpenApi.Models;

namespace MockHotelsAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            /// <summary>
            /// Opretter en builder som bruges til at konfigurere applikationen.
            /// </summary>
            var builder = WebApplication.CreateBuilder(args);

            /// <summary>
            /// Registrerer support til controllers i ASP.NET Core.
            /// Gør det muligt at bruge [ApiController]-baserede endpoints som HotelsController.cs
            /// </summary>
            builder.Services.AddControllers();

            /// <summary>
            /// Tilføjer Swagger (OpenAPI) dokumentation og test-værktøj.
            /// Swagger dokumenterer automatisk alle endpoints i API'et.
            /// </summary>
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Mock Hotels API",
                    Version = "v1",
                    Description = "En mock API til hoteldata med fiktive ophold og faciliteter."
                });
            });

            /// <summary>
            /// Bygger appen med de services og konfigurationer der er blevet tilføjet.
            /// </summary>
            var app = builder.Build();

            /// <summary>
            /// Aktiver Swagger og Swagger UI i udviklingsmiljø.
            /// Det giver mulighed for at teste API’et via browseren.
            /// </summary>
            if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Mock Hotels API v1");
                    c.RoutePrefix = string.Empty;
                });
            }

            /// <summary>
            /// Autorisation middleware – forberedt til fremtidig sikkerhed.
            /// </summary>
            app.UseAuthorization();

            /// <summary>
            /// Aktiverer controller-routing, så API-endpoints kan tilgås.
            /// </summary>
            app.MapControllers();

            /// <summary>
            /// Lytter på port 8080 for HTTP-anmodninger.
            /// </summary>
            var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
            app.Urls.Add($"http://+:{port}");
            Console.WriteLine($"Running on port: {Environment.GetEnvironmentVariable("PORT")}");


            /// <summary>
            /// Starter applikationen og gør den klar til at modtage kald.
            /// </summary>
            app.Run();
        }
    }
}
