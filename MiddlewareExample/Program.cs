using Assistt.AspNetCore.PerformaceMiddleware.Core;
using Microsoft.AspNetCore.Http;
using MiddlewareExample.Middlewares;
using MiddlewareExample.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Factory middleware
builder.Services.AddTransient<FactoryMiddleware>();
builder.Services.AddScoped<ISample,SampleService>();

var app = builder.Build();




//app.Use(async (context, next) =>
//{
//  await next();
//});
//app.Run(async context =>
//{
//  await context.Response.WriteAsync("Run Middleware");
//});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
  app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
// yetkilendirme öncesi iþlemler
app.UseAuthorization();


#region MiddlewareTypes

/*
app.Use(async (context, next) =>
{
  // Request
  Console.WriteLine("Request");
  // Ýkisi birlikte kullanýmasýn  OnStarting  await next(context); öncesinde kullanabiliriz.


  //context.Response.OnStarting(() =>
  //{
  //  // Response burada mudehale edebilirz
  //  return Task.CompletedTask;
  //});

  await next(context);
  // next() sonrasýnda süreç reponse dönüyor ilk reponse dönerken bazen header güncellmesi vs yapmak gerekirse burda kontrollü bir þekilde yapabiliriz.
  if (context.Response.HasStarted)
  {
    Console.WriteLine("Response");
  }


  context.Response.OnCompleted(() =>
  {
    // Not Responsedaki herhangi bir header deðiþtirilemez.
    // Burada response artýk sonlandýðý response header cookie oluþturma vs gibi kýsýmlara müdehale demeyiz.
    Console.WriteLine("Completed oldu");
    return Task.CompletedTask;
  });


});

*/

/*
app.Map("/api/clients", app1 =>
{
  // route bazlý çalýþýr
  app1.Use(async (context, next) =>
  {
    Console.WriteLine("map app1 Request");
    await context.Response.WriteAsync("map app1 request 1 \n");

    await next();
 
    await context.Response.WriteAsync("map app1 response 1 \n");
    

  });

  app1.Use(async (context, next) =>
  {
    Console.WriteLine("map app1 2");
    await context.Response.WriteAsync("map app1 request 2 \n");


    await next();

   
    await context.Response.WriteAsync("map app1 response 2 \n");
    
   
  });

  // uygulama buradan sonlasýn diyip kendi pipeline da sonlandýrma yaparýz.
  //app.Run();

});


// useWhen ve mapWhen kendi içinde bir pipeline oluþturur. fakat aralarýndaki fark useWhen kendi pipeline bitirsince aþaðýdaki middlewarelerin çalýþmasýna devam etmek için süreci devreder.
app.UseWhen(context => context.Request.Path.StartsWithSegments("/api/usewhen"), app1 =>
{
  app1.Use(async (context, next) =>
  {
    await context.Response.WriteAsync("when req 1 \n");
    await next();
    await context.Response.WriteAsync("when use res 1 \n");
  });
});


app.MapWhen(context => context.Request.Path.StartsWithSegments("/api/mapWhen"), app1 =>
{
  app1.Use(async (context, next) =>
  {
    await context.Response.WriteAsync("map when req 1 \n");
    await next();
    await context.Response.WriteAsync("map when use res 1 \n");
  });
});

app.Use(async (context, next) =>
{
  await context.Response.WriteAsync("app use 1 \n");
  await next();

  await context.Response.WriteAsync("app use 2 \n");
});


// Items Dictorany üzerinden request de baþka bir middleware veri gönderim örneði

app.Use(async (context, next) =>
{
  if (context.Request.Path.StartsWithSegments("/api/data-transfer"))
  {
    // bir iþlem yapýp geç
    context.Items["Message"] = "Context Item Data \n";
    await next();
  } 
  else
  {
    // her seferin bu middleware çalýþacaðý için altta olan middlware hiç bir iþlem yapmadan geç
    await next();
  }
});

app.Use(async (context, next) =>
{
  if (context.Items.ContainsKey("Message"))
  {
   
    if(context.Items["Message"] != null)
    {
      await context.Response.WriteAsync(context.Items["Message"]?.ToString());
      // Not middlewarelerde hata durumlarýnda next ile sonraki midldeware geçmeden süreci kesmek için return kullanýrýz. 401, 403, 500, 400, 429
     //return;
      await next();
    }
    else
    {
      await next();
    }
  }
  else
  {
    await next();
  }



});

*/

#endregion

//app.Map("/api/custom-middle", app =>
//{
//  app.UseMiddleware<RequestLoggingMiddleware>();
//});

//app.UseMiddleware<RequestLoggingMiddleware>();
// app.UseRequestLogging();
//app.UseRequestBenchMark();

//app.UseMiddleware<FactoryMiddleware>();

app.UseMiddleware<ScopeMiddleware>();

// Kimlik doðrulama sonrasý middleware kullanýmý

app.MapControllers();

app.Run();
