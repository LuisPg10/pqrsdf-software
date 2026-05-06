using API.Extensions;
using API.Services;
using Application.Services;
using Infraestructure.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddOpenApi();

// Adding services for each layer
builder.Services
  .AddPresentation()
  .AddInfraestructure(builder.Configuration)
  .AddApplication();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.ApplyMigrations();
  app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();