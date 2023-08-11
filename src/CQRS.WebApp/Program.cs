using System.Diagnostics;
using CQRS.WebApp;
using Oakton;
using Serilog;
using Wolverine;
using Wolverine.FluentValidation;

var builder = WebApplication.CreateBuilder(args);
builder.Host
  .UseSerilog((context, configuration) => configuration
      .ReadFrom.Configuration(context.Configuration)
      .Enrich.FromLogContext()
      .Enrich.WithCorrelationId()
      .Enrich.WithCorrelationIdHeader()
      .Enrich.WithProperty("ApplicationName", typeof(Program).Assembly.GetName().Name!)
      .Enrich.WithProperty("Environment", context.HostingEnvironment)
      .Enrich.WithProperty("DebuggerAttached", Debugger.IsAttached))
  .UseWolverine(options =>
  {
    options.Discovery.IncludeAssembly(typeof(Program).Assembly);
    options.UseFluentValidation();
  })
  .ApplyOaktonExtensions();

builder.Services.AddProblemDetails();

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddSpaStaticFiles(configuration => { configuration.RootPath = "wwwroot/dist"; });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
  app.UseExceptionHandler("/Home/Error");
  // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
  app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSpaStaticFiles();
app.UseSpaStaticFiles(new StaticFileOptions()
{
  OnPrepareResponse = context =>
  {
    // Cache static file for 1 year
    if (bool.Parse(app.Configuration["Setup:EnableCache"]))
    {
      context.Context.Response.Headers.Add("cache-control", new[] {"private,max-age=432000"});
      context.Context.Response.Headers.Add("Expires",
        new[] {DateTime.UtcNow.AddDays(5).ToString("R")}); // Format RFC1123
    }
  }
});

app.UseSpa(spa =>
{
  spa.Options.SourcePath = ".";
  spa.Options.DefaultPageStaticFileOptions = new StaticFileOptions()
  {
    OnPrepareResponse = ctx =>
    {
      var headers = ctx.Context.Response.GetTypedHeaders();
      headers.CacheControl = new Microsoft.Net.Http.Headers.CacheControlHeaderValue
      {
        Public = false,
        NoCache = true,
        MaxAge = TimeSpan.FromMinutes(-2)
      };
    }
  };

  if (app.Environment.IsDevelopment())
  {
    var devServer = app.Configuration["Spa:DevServer"];
    spa.UseProxyToSpaDevelopmentServer(devServer);
  }
});


app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
  name: "default",
  pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
