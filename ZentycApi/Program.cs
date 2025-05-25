using Microsoft.EntityFrameworkCore;
using Zentyc.Data;

var builder = WebApplication.CreateBuilder(args);

// Configuración de la base de datos
builder.Services.AddDbContext<WebContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("WebContext")));



// Habilitar CORS (opcional, para desarrollo)
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// Configuración de Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "Tu API", Version = "v1" });
});

builder.Services.AddControllers();

var app = builder.Build();

// Middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors(); // Solo si habilitaste CORS
app.UseAuthorization();
app.MapControllers();
app.Run();