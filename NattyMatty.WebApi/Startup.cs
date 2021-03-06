﻿using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NattyMatty.WebApi.Models;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Swagger;
using System.Reflection;
using System.IO;
using System;
using Microsoft.Extensions.Logging.Debug;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;
using NattyMatty.WebApi.Data;
using Microsoft.AspNetCore.SpaServices.AngularCli;

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

            builder.AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {            
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

            services.AddLogging(builder => builder
                .AddConsole()
                .AddDebug()
                .AddFilter("System", LogLevel.Information) // Rule for all providers
                .AddFilter<DebugLoggerProvider>("Microsoft", LogLevel.Trace) // Rule only for debug provider
                .AddConfiguration(Configuration.GetSection("Logging")));

            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {    
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "NattyMatty API V1");
                //c.RoutePrefix = string.Empty;
            });           

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

                routes.MapSpaFallbackRoute(
                    name: "spa-fallback",
                    defaults: new { controller = "Home", action = "Index" });
            });

            app.UseDefaultFiles();

			app.UseStaticFiles();
			
			app.UseSpaStaticFiles();

            /*
#if DEBUG
            //https://stackoverflow.com/questions/32057441/disable-application-insights-in-debug
            TelemetryConfiguration.Active.DisableTelemetry = true;
#endif*/
            //https://github.com/aspnet/Home/issues/2051
            //var configuration = app.ApplicationServices.GetService<TelemetryConfiguration>();
            //configuration.DisableTelemetry = true;

            // Create a service scope to get an ProductContext instance using DI
            using (var serviceScope =
                app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetService<ProductContext>();
                /* */
                // Create the Db if it doesn't exist and applies any pending migration.
                dbContext.Database.Migrate();
                // Seed the Db.
                DbSeeder.Seed(dbContext);
            }            
			
			app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    //spa.UseAngularCliServer(npmScript: "start");
					spa.UseProxyToSpaDevelopmentServer("http://localhost:4200");
                }
            });
        }
    }
}