using Microsoft.EntityFrameworkCore;
using evojacu.Models;
using evojacu.Services; // Dodajte ovo za pristup UserService

var config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", false)
    .Build();

var builder = WebApplication.CreateBuilder(args);

// Dodajte DbContext i SQL Server podršku
builder.Services.AddDbContext<evojacuDBContext>(options =>
    options.UseSqlServer(config.GetConnectionString("evojacu")));

// Registrujte UserService
builder.Services.AddScoped<UserService>();

// CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowOrigin", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

// Dodajte usluge u kontejner
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Konfigurišite HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowOrigin"); // CORS policy

app.UseAuthorization();

app.MapControllers();

app.Run();
