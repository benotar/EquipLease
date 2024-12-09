using EquipLease.Application;
using EquipLease.Application.Services;
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

// Async background processor
builder.Services.AddConfiguredQueueClient(builder.Configuration);
builder.Services.AddHostedService<EquipBackgroundService>();

// Swagger
builder.Services.AddSwagger(builder.Configuration);

// API Key filtering
builder.Services.AddApiKeyFilter();

var app = builder.Build();

app.UseAuthorization();

// Swagger
app.UseSwagger();
app.UseSwaggerUI(config =>
{
    config.RoutePrefix = string.Empty;

    config.SwaggerEndpoint("swagger/v1/swagger.json", "EquipLease API");
});


// Use exceptions handling
app.UseExceptionHandler();

app.MapControllers();

// Standard route for the home page
app.MapGet("/", () => $"Welcome to the Home Page EquipLease API!\nUTC Time: {DateTime.UtcNow}");

app.Run();