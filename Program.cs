using Microsoft.EntityFrameworkCore;
using TelemedicinaApi.Services;
using TelemedicinaAPI.Data;
using TelemedicinaApi.ExternalServices;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseInMemoryDatabase("TelemedicinaDb"));

builder.Services.AddScoped<ConsultaService>();
builder.Services.AddScoped<MedicoService>();
builder.Services.AddScoped<PacienteService>();

builder.Services.AddHttpClient<DistanceService>();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    DbInitializer.Initialize(dbContext); 
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
app.UseAuthorization();
app.MapControllers();
app.Run();