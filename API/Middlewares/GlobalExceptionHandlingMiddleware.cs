using System.Net;
using System.Text.Json;

namespace API.Middlewares;

public class GlobalExceptionHandlingMiddleware(ILogger<GlobalExceptionHandlingMiddleware> logger) : IMiddleware
{
  public async Task InvokeAsync(HttpContext context, RequestDelegate next)
  {
    try
    {
      await next(context);
    }
    catch (Exception ex)
    {
      logger.LogError(ex, ex.Message);

      context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

      ProblemDetails problema = new ProblemDetails()
      {
        Status = (int)HttpStatusCode.InternalServerError,
        Type = "Error in the server.",
        Title = "Error in the server.",
        Detail = "A internal error has ocurred into the server."
      };

      var json = JsonSerializer.Serialize(problema);

      context.Response.ContentType = "application/json";

      await context.Response.WriteAsync(json);
    }
  }
}