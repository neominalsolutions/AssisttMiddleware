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
// yetkilendirme �ncesi i�lemler
app.UseAuthorization();


#region MiddlewareTypes

/*
app.Use(async (context, next) =>
{
  // Request
  Console.WriteLine("Request");
  // �kisi birlikte kullan�mas�n  OnStarting  await next(context); �ncesinde kullanabiliriz.


  //context.Response.OnStarting(() =>
  //{
  //  // Response burada mudehale edebilirz
  //  return Task.CompletedTask;
  //});

  await next(context);
  // next() sonras�nda s�re� reponse d�n�yor ilk reponse d�nerken bazen header g�ncellmesi vs yapmak gerekirse burda kontroll� bir �ekilde yapabiliriz.
  if (context.Response.HasStarted)
  {
    Console.WriteLine("Response");
  }


  context.Response.OnCompleted(() =>
  {
    // Not Responsedaki herhangi bir header de�i�tirilemez.
    // Burada response art�k sonland��� response header cookie olu�turma vs gibi k�s�mlara m�dehale demeyiz.
    Console.WriteLine("Completed oldu");
    return Task.CompletedTask;
  });


});

*/

/*
app.Map("/api/clients", app1 =>
{
  // route bazl� �al���r
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

  // uygulama buradan sonlas�n diyip kendi pipeline da sonland�rma yapar�z.
  //app.Run();

});


// useWhen ve mapWhen kendi i�inde bir pipeline olu�turur. fakat aralar�ndaki fark useWhen kendi pipeline bitirsince a�a��daki middlewarelerin �al��mas�na devam etmek i�in s�reci devreder.
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


// Items Dictorany �zerinden request de ba�ka bir middleware veri g�nderim �rne�i

app.Use(async (context, next) =>
{
  if (context.Request.Path.StartsWithSegments("/api/data-transfer"))
  {
    // bir i�lem yap�p ge�
    context.Items["Message"] = "Context Item Data \n";
    await next();
  } 
  else
  {
    // her seferin bu middleware �al��aca�� i�in altta olan middlware hi� bir i�lem yapmadan ge�
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
      // Not middlewarelerde hata durumlar�nda next ile sonraki midldeware ge�meden s�reci kesmek i�in return kullan�r�z. 401, 403, 500, 400, 429
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

// Kimlik do�rulama sonras� middleware kullan�m�

app.MapControllers();

app.Run();
