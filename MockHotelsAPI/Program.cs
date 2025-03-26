using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

/// <summary>
/// Registrerer controller-support i ASP.NET Core.
/// Det gør det muligt at bruge attribut-baserede controllers,
/// som f.eks. HotelsController.cs, til at definere REST-endpoints.
/// </summary>
builder.Services.AddControllers(); // Aktiverer brug af [ApiController]-baserede controllers

/// <summary>   
/// CORS står for "Cross-Origin Resource Sharing" og er en teknik, 
/// som gør det muligt at anmode om ressourcer fra en anden kilde end den, der leverer websiden.
/// I dette tilfælde tillader vi adgang for vores Blazor-frontend, som kører på en anden port.
/// Blazor-frontend'en kører på https://localhost:7177, mens API'et kører på https://localhost:7121.
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
/// Gør det muligt at bruge Swagger UI – interaktiv dokumentation og testværktøj.
/// Swagger vil automatisk dokumentere alle dine API-endpoints.
/// </summary>
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "MockHotelsAPI",
        Version = "v1",
        Description = "En mock API til test og udvikling af hotelsøgning" // i Gotorz
    });
});

var app = builder.Build();

/// <summary>
/// Aktiverer Swagger og Swagger UI – kun i udviklingsmiljø.
/// Det betyder, at du kan se dokumentationen på /swagger, når du kører lokalt.
/// </summary>
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "MockHotelsAPI v1");
        c.RoutePrefix = string.Empty; // Gør at Swagger åbnes direkte i browseren
    });
}

/// <summary>
/// Aktiverer CORS med den policy vi definerede ovenfor ("AllowBlazorFrontend").
/// Dette skal stå før MapControllers, så anmodninger bliver korrekt håndteret.
/// </summary>
app.UseCors("AllowBlazorFrontend");

/// <summary>
/// Bruges til autorisation – selvom vi ikke bruger det endnu,
/// skal det være med i pipelinen, hvis vi tilføjer sikkerhed senere.
/// </summary>
app.UseAuthorization();

/// <summary>
/// Mapper controller-ruter, som f.eks. GET /api/hotels,
/// så API'en kan modtage HTTP-anmodninger.
/// </summary>
app.MapControllers();

/// <summary>
/// Starter hele webapplikationen.
/// </summary>
app.Run();
