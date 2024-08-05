using MiddlewareExample.Services;

namespace MiddlewareExample.Middlewares
{
  // Factory middlewareleri Transient olarak service provider'a tanımlıyoruz.
  // Contructor Injection
  public class FactoryMiddleware : IMiddleware
  {
    private readonly ISample _sample;

    public FactoryMiddleware(ISample sample)
    {
      _sample = sample;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
       await _sample.ExecuteAsync();
      await next(context);
    }
  }
}
