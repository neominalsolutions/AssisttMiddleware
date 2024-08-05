using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assistt.AspNetCore.PerformaceMiddleware.Core
{
  public static class MiddlewareExtensions
  {
    public static void UseRequestBenchMark(this IApplicationBuilder applicationBuilder)
    {
      applicationBuilder.UseMiddleware<RequestBenchMarkMiddleware>();
    }
  }
}
