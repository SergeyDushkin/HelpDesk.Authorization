using IdentityServer4.Services;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Security.Cryptography.X509Certificates;
using System.IO;
using Serilog;
using authorization.Dal;

namespace authorization
{
    public class Startup
    {
        private readonly IHostingEnvironment Environment;
        private readonly IConfigurationRoot Configuration;

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
            Environment = env;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<UserDbContext>(options => options.UseNpgsql(Configuration.GetConnectionString("AuthorizationDatabase")));

            var cert = new X509Certificate2(Path.Combine(Environment.ContentRootPath, "IdentityHelpDesk.pfx"), "12345678");
            //var key = System.Text.Encoding.UTF8.GetBytes("c8Ez0kTUjBirHdFF3kY3YNqm2CXu2tODZaO4gsNRjlHhu55FL7axYchfFxqz0Iv"); 

            services.AddIdentityServer()
                .AddSigningCredential(cert)
                .AddInMemoryClients(Clients.Get())
                .AddInMemoryApiResources(ApiResources.Get());

            services.AddTransient<IResourceOwnerPasswordValidator, ResourceOwnerPasswordValidator>();
            services.AddTransient<IProfileService, ProfileService>();
            services.AddTransient<IProfileManager, ProfileManager>();

            services.AddScoped<IDatabaseSeeder, DatabaseInitializer>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            //Func<string, LogLevel, bool> filter = (scope, level) =>
            //    scope.StartsWith("IdentityServer") ||
            //    scope.StartsWith("IdentityModel") ||
            //    level == LogLevel.Error ||
            //    level == LogLevel.Critical;

             var serilogLogger = new LoggerConfiguration()
                .Enrich.WithProperty("Application", "ServiceDesk.Services.Authorization")
                .ReadFrom.Configuration(Configuration)
                .CreateLogger();

            loggerFactory.AddSerilog(serilogLogger);
            loggerFactory.AddConsole();   //filter

            app.UseDeveloperExceptionPage();

            app.UseCors(option => option
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowAnyOrigin()
                .AllowCredentials());
                
            app.ApplicationServices.GetService<UserDbContext>().Database.EnsureDeleted();
            if (app.ApplicationServices.GetService<UserDbContext>().Database.EnsureCreated())
            {
                app.ApplicationServices.GetService<IDatabaseSeeder>().SeedAsync();
            }

            app.UseIdentityServer();
        }
    }
}
