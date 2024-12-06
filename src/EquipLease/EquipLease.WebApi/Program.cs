using EquipLease.Persistence;
using EquipLease.WebApi;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCustomConfigurations(builder.Configuration);

builder.Services.AddPersistence(builder.Configuration);

builder.Services.AddControllers();

var app = builder.Build();

app.UseAuthorization();

app.MapControllers();

// Standard route for the home page
app.MapGet("/", () => $"Welcome to the Home Page EquipLease API!\nUTC Time: {DateTime.UtcNow}");

app.Run();