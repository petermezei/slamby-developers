using DevelopersSite.Helpers;
using DevelopersSite.Models;
using DevelopersSite.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace DevelopersSite
{
    public class Startup
    {
        private string ApiVersion = VersionHelper.GetProductVersion(typeof(Startup));
        private bool IsProduction;

        public Startup(IHostingEnvironment env)
        {
            IsProduction = env.IsProduction();

            // Set up configuration sources.
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json")
                .AddJsonFile("appsettings.production.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc();

            services.AddDistributedMemoryCache();

            services.AddOptions();
            services.Configure<SiteConfig>(Configuration.GetSection("SiteConfig"));
            services.Configure<SiteConfig>(sc => sc.Version = ApiVersion);
            if (IsProduction)
            {
                services.Configure<SiteConfig>(sc => sc.CdnUrl = string.Format(sc.CdnUrl, ApiVersion));
            }
            else
            {
                services.Configure<SiteConfig>(sc => sc.CdnUrl = string.Empty);
            }

            services.AddSingleton<SiteConfig>(sp => sp.GetService<IOptions<SiteConfig>>().Value);
            services.AddSingleton<DocumentService>();
            services.AddSingleton<WordPressService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                   name: "default",
                   template: "{controller=home}/{action=index}");
            });            
        }
    }
}
