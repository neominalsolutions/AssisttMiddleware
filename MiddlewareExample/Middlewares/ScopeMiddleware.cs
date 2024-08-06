using MiddlewareExample.Services;

namespace MiddlewareExample.Middlewares
{
  public class ScopeMiddleware
  {
    private readonly RequestDelegate _next;
    private readonly ISample _sample;

    // Transient tanımlı servislerde DI Contructor vasıtası ile geçilebilir fakat Scoped servisler Injection Method üzerinden aşağıdaki gibi yapılmalıdır
    //public ScopeMiddleware(RequestDelegate next, ISample sample)
    //{
    //  _sample = sample;
    //  _next = next;
    //}

    public ScopeMiddleware(RequestDelegate next)
    {
      _next = next;

    }

    public async Task InvokeAsync(HttpContext context, ISample sample)
    {
      // service Provider üzerinden service look up işlemi.
      var service = context.RequestServices.GetRequiredService<ISample>();
      await sample.ExecuteAsync();
      await service.ExecuteAsync();
      await _next(context);
    }
  }
}
