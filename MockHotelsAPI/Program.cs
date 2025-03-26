using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

/// <summary>
/// Registrerer controller-support i ASP.NET Core.
/// Det g�r det muligt at bruge attribut-baserede controllers,
/// som f.eks. HotelsController.cs, til at definere REST-endpoints.
/// </summary>
builder.Services.AddControllers(); // Aktiverer brug af [ApiController]-baserede controllers

/// <summary>   
/// CORS st�r for "Cross-Origin Resource Sharing" og er en teknik, 
/// som g�r det muligt at anmode om ressourcer fra en anden kilde end den, der leverer websiden.
/// I dette tilf�lde tillader vi adgang for vores Blazor-frontend, som k�rer p� en anden port.
/// Blazor-frontend'en k�rer p� https://localhost:7177, mens API'et k�rer p� https://localhost:7121.
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.WithOrigins("https://localhost:7177") //Tillader kun anmodninger fra denne URL
            .AllowAnyHeader() // Tillad alle headers
            .AllowAnyMethod(); // Tillad alle HTTP-metoder (GET, POST, PUT, DELETE, mm.)
    });
});

/// <summary>
/// G�r det muligt at bruge Swagger UI � interaktiv dokumentation og testv�rkt�j.
/// Swagger vil automatisk dokumentere alle dine API-endpoints.
/// </summary>
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "MockHotelsAPI",
        Version = "v1",
        Description = "En mock API til test og udvikling af hotels�gning" // i Gotorz
    });
});

var app = builder.Build();

/// <summary>
/// Aktiverer Swagger og Swagger UI � kun i udviklingsmilj�.
/// Det betyder, at du kan se dokumentationen p� /swagger, n�r du k�rer lokalt.
/// </summary>
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "MockHotelsAPI v1");
        c.RoutePrefix = string.Empty; // G�r at Swagger �bnes direkte i browseren
    });
}

/// <summary>
/// Aktiverer CORS med den policy vi definerede ovenfor ("AllowBlazorFrontend").
/// Dette skal st� f�r MapControllers, s� anmodninger bliver korrekt h�ndteret.
/// </summary>
app.UseCors("AllowBlazorFrontend");

/// <summary>
/// Bruges til autorisation � selvom vi ikke bruger det endnu,
/// skal det v�re med i pipelinen, hvis vi tilf�jer sikkerhed senere.
/// </summary>
app.UseAuthorization();

/// <summary>
/// Mapper controller-ruter, som f.eks. GET /api/hotels,
/// s� API'en kan modtage HTTP-anmodninger.
/// </summary>
app.MapControllers();

/// <summary>
/// Starter hele webapplikationen.
/// </summary>
app.Run();
