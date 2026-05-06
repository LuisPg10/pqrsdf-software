using API.Config.Scalar;
using API.Extensions;
using API.Services;
using Application.Services;
using Infraestructure.Services;
using Microsoft.OpenApi;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddOpenApi(options =>
{
  options.AddDocumentTransformer((document, _, _) =>
  {
    document.Components ??= new OpenApiComponents();
    document.Components.SecuritySchemes = new Dictionary<string, IOpenApiSecurityScheme>
    {
      ["BearerAuth"] = new OpenApiSecurityScheme
      {
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        Description = "Type your Jwt"
      }
    };
    
    return Task.CompletedTask;
  });
});

// Adding services for each layer
builder.Services
  .AddPresentation(builder.Configuration)
  .AddInfraestructure(builder.Configuration)
  .AddApplication();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.ApplyMigrations();
  app.MapOpenApi();
  ScalarConfig.Config(app);
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();