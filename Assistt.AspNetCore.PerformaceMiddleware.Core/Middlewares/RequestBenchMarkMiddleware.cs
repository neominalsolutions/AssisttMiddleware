using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace Assistt.AspNetCore.PerformaceMiddleware.Core
{
  // Convensional Class Middleware
  public class RequestBenchMarkMiddleware
  {
    private readonly RequestDelegate _next;
    private readonly ILogger<RequestBenchMarkMiddleware> _logger;

    public RequestBenchMarkMiddleware(RequestDelegate next, ILogger<RequestBenchMarkMiddleware> logger)
    {
      _next = next;
      _logger = logger;
    }
    public async Task Invoke(HttpContext context)
    {

      var sp = Stopwatch.StartNew();

      _logger.LogInformation($"Request Started Time {sp.ElapsedMilliseconds}, Path {context.Request.Path}, Port {context.Request.Host.Port} ");

      // aşağıdaki şekilde çağırmayalım bu Main threadi bloklar.
      // _next(context).Wait();

      await _next(context);

      sp.Stop();
      _logger.LogInformation($"Response Started Time {sp.ElapsedMilliseconds}, Path {context.Request.Path}, Port {context.Request.Host.Port} ");
    }


  }
}
