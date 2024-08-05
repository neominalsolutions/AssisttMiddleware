namespace MiddlewareExample.Services
{
  public class SampleService : ISample
  {
    public Task ExecuteAsync()
    {
      Console.WriteLine("Executed");
      return Task.CompletedTask;
    }
  }
}
