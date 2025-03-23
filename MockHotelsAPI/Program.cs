using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Tilf�j n�dvendige services til Web API og Swagger
builder.Services.AddControllers(); // Aktiverer brug af [ApiController]-baserede controllers

// Tilf�jer OpenAPI/Swagger dokumentation
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "MockHotelsAPI",
        Version = "v1",
        Description = "En mock API til test og udvikling af hotels�gning"
    });
});

var app = builder.Build();

// Aktiver Swagger i udviklingsmilj�
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "MockHotelsAPI v1");
        c.RoutePrefix = string.Empty; // G�r at Swagger �bnes direkte i browseren
    });
}

// Routing og controller-aktivering
app.UseAuthorization();
app.MapControllers();

app.Run();
