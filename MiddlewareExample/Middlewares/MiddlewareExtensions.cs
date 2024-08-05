using System.Runtime.CompilerServices;

namespace MiddlewareExample.Middlewares
{
  public static class MiddlewareExtensions
  {
    public static void UseRequestLogging(this IApplicationBuilder applicationBuilder)
    {
  applicationBuilder.UseMiddleware<RequestLoggingMiddleware>();
    }

    public static void UseResponseLogging(this IApplicationBuilder applicationBuilder)
    {
      applicationBuilder.UseMiddleware<RequestLoggingMiddleware>();
    }
  }
}
