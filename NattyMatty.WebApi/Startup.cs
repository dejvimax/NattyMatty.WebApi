using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NattyMatty.WebApi.Models;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Swagger;
using System.Reflection;
using System.IO;
using System;
using Microsoft.AspNetCore.Http;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.Extensions.Logging.Debug;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;

namespace NattyMatty.WebApi
{
    public class Startup
    {
        public IConfigurationRoot Configuration { get; }

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

            //if (env.IsEnvironment("Development"))
            //{
            //    // This will push telemetry data through Application Insights pipeline faster, allowing you to view results immediately.
            //    builder.AddApplicationInsightsSettings(developerMode: true);
            //}

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            /*
            var connection = @"Server=HOME_MEDIA_PC\SQLEXPRESS;Database=NattyMattyDb;Trusted_Connection=True;ConnectRetryCount=0";
            services.AddDbContext<ProductContext>(options => options.UseSqlServer(connection));
             */
            //services.AddDbContext<ProductContext>(opt => opt.UseInMemoryDatabase("ProductList"));

            services.AddMvc();

            services.AddEntityFrameworkSqlServer();

            services.AddDbContext<ProductContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            // Register the Swagger generator, defining one or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "Natty Matty API",
                    Description = "The Natty Matty ASP.NET Core Web API",
                    TermsOfService = "None",
                    Contact = new Contact
                    {
                        Name = "dejvimax",
                        Email = "davie_max@hotmail.com"
                    },
                    License = new License
                    {
                        Name = "Use under LICX",
                        Url = "https://example.com/license"
                    }
                });

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetEntryAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                //TODO: this section of code breaks Azure deployment
                //c.IncludeXmlComments(xmlPath);
            });

            //IConfiguration configuration = new ConfigurationBuilder()
            //.SetBasePath(env.ContentRootPath)
            //.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);


            services.AddLogging(builder => builder
                .AddConsole()
                .AddDebug()
                .AddFilter("System", LogLevel.Information) // Rule for all providers
                .AddFilter<DebugLoggerProvider>("Microsoft", LogLevel.Trace) // Rule only for debug provider
                .AddConfiguration(Configuration.GetSection("Logging")));

            
        }

        public void Configure(IApplicationBuilder app,
            ILoggerFactory loggerFactory)
        {
            /*loggerFactory
                .AddConsole()
                .AddDebug();*/

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "NattyMatty API V1");
                //c.RoutePrefix = string.Empty;
            });

            app.UseMvc();

            app.Run(async (context) =>
            {
                /*var logger = loggerFactory.CreateLogger("NattyMatty.WebApi.Startup");
                logger.LogTrace("Hello world : Trace");
                logger.LogDebug("Hello world : Debug");
                logger.LogInformation("Hello world : Information");
                logger.LogError("Hello world : Error");
                logger.LogInformation("No endpoint found for request {path}", context.Request.Path);*/
                await context.Response.WriteAsync("No endpoint found - try /api/todo.");
            });

#if DEBUG
            //https://stackoverflow.com/questions/32057441/disable-application-insights-in-debug
            TelemetryConfiguration.Active.DisableTelemetry = true;
#endif
            //https://github.com/aspnet/Home/issues/2051
            //var configuration = app.ApplicationServices.GetService<TelemetryConfiguration>();
            //configuration.DisableTelemetry = true;


        }
    }
}