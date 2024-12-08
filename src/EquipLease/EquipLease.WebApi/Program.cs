using EquipLease.Application;
using EquipLease.Persistence;
using EquipLease.WebApi;

var builder = WebApplication.CreateBuilder(args);

// Custom configurations
builder.Services.AddCustomConfigurations(builder.Configuration);

// Application layers
builder.Services
    .AddApplication()
    .AddPersistence(builder.Configuration);

// Configured controllers
builder.Services.AddControllersWithConfiguredApiBehavior(builder.Configuration);

// Exceptions handling
builder.Services.AddExceptionHandlerWithProblemDetails();

var app = builder.Build();

app.UseAuthorization();

// Use exceptions handling
app.UseExceptionHandler();

app.MapControllers();

// Standard route for the home page
app.MapGet("/", () => $"Welcome to the Home Page EquipLease API!\nUTC Time: {DateTime.UtcNow}");

app.Run();