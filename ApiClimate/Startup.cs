using ApiClimate;

//Startando Class Program 
var builder = WebApplication.CreateBuilder(args);
//Startando Class Program 
var startup = new Startup(builder.Configuration);

startup.ConfigurationServices(builder.Services);
var app = builder.Build();


startup.Configure(app, app.Environment);
app.Run();

// Configure the HTTP request pipeline.

IHostBuilder CreateHostBuilder(string[] args) =>
 Host.CreateDefaultBuilder(args)
     .ConfigureWebHostDefaults(webBuilder =>
     {

         webBuilder.UseStartup<Startup>();

     });

