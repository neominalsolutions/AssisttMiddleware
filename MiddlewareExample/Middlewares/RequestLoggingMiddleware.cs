using MiddlewareExample.Services;

namespace MiddlewareExample.Middlewares
{
  // Convensional Class Middleware
  public class RequestLoggingMiddleware
  {
    private readonly RequestDelegate _next;
    private readonly ILogger<RequestLoggingMiddleware> _logger;
    //private readonly IServiceProvider _serviceProvider;
    //private readonly ISample _sample;

    // Convension Based Classlarda kendi servislerimizi Contructor Injection yapamıyoruz. Method Injection yapıyoruz.

    public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
    {
      _next = next;
      _logger = logger;
      // Service LookUp
      
      //_sample = _serviceProvider.GetRequiredService<ISample>();
      
    }
    public async Task Invoke(HttpContext context, ISample sample)
    {

      var sample2 =  context.RequestServices.GetRequiredService<ISample>();

      _logger.LogInformation("Request Started");

      // aşağıdaki şekilde çağırmayalım bu Main threadi bloklar.
      // _next(context).Wait();

      await sample.ExecuteAsync();

      await _next(context);

      _logger.LogInformation("Response Started");
    }


  }
}
