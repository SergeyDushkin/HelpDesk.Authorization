using IdentityServer4.Services;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace authorization
{
    public class Startup
    {
        private readonly IHostingEnvironment _environment;
        private readonly IConfigurationRoot _configuration;

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            _configuration = builder.Build();
            _environment = env;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<UserDbContext>(options => options.UseSqlServer(_configuration.GetConnectionString("AuthorizationDatabase")));

            //var cert = new X509Certificate2(Path.Combine(_environment.ContentRootPath, "IdentityHelpDesk.pfx"), "12345678");
            //var key = System.Text.Encoding.UTF8.GetBytes("c8Ez0kTUjBirHdFF3kY3YNqm2CXu2tODZaO4gsNRjlHhu55FL7axYchfFxqz0Iv"); 

            services.AddIdentityServer()
                .AddTemporarySigningCredential()
                .AddInMemoryClients(Clients.Get())
                .AddInMemoryApiResources(ApiResources.Get());

            services.AddTransient<IResourceOwnerPasswordValidator, ResourceOwnerPasswordValidator>();
            services.AddTransient<IProfileService, ProfileService>();
            services.AddTransient<IProfileManager, ProfileManager>();
        }

        public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            Func<string, LogLevel, bool> filter = (scope, level) =>
                scope.StartsWith("IdentityServer") ||
                scope.StartsWith("IdentityModel") ||
                level == LogLevel.Error ||
                level == LogLevel.Critical;

            loggerFactory.AddConsole(filter);
            loggerFactory.AddDebug(filter);

            app.UseDeveloperExceptionPage();

            app.UseCors(option => option
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowAnyOrigin()
                .AllowCredentials());

            app.UseIdentityServer();
        }
    }
}
