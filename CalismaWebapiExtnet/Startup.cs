using Ext.Net;
using Ext.Net.Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;
using Westwind.AspNetCore.LiveReload;

namespace CalismaWebapiExtnet
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<GzipCompressionProviderOptions>(options =>
            {
                options.Level = System.IO.Compression.CompressionLevel.Optimal;
            });

            services.AddResponseCompression(options =>
            {
                options.EnableForHttps = true;
                options.Providers.Add<GzipCompressionProvider>();

                options.MimeTypes = new[]
                {
                    "text/css",
                    "application/javascript",
                    "application/json",
                    "text/json",
                    "application/xml",
                    "text/xml",
                    "text/plain",
                    "image/svg+xml",
                    "application/x-font-ttf"
                };
            });

            services.AddRazorPages().AddRazorRuntimeCompilation();
            services.AddRouting(config =>
            {
                config.LowercaseUrls = true;
            });

            // See https://github.com/RickStrahl/Westwind.AspnetCore.LiveReload
            services.AddLiveReload();

            // 1. Register Ext.NET services
            services.AddExtNet();
            services.AddExtCharts();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseLiveReload();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseResponseCompression();

            // 2. Use Ext.NET resources
            //    To be added prior to app.UseStaticFiles()
            app.UseExtNetResources(config =>
            {
                if (env.IsDevelopment())
                {
                    config.UseDebug(true);
                }

                config.UseEmbedded();
                config.UseCharts();
                config.UseThemeSpotless();
            });

            // 3. Enable Ext.NET localization [not required]
            //    If included, localization will be handled automatically
            //    based on client browser preferences
            app.UseExtNetLocalization();

            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();

            // 4. Ext.NET middleware
            //    To be added prior to app.UseEndpoints()
            app.UseExtNet(config =>
            {
                config.Theme = ThemeKind.Spotless;
            });

            app.UseEndpoints(endpoints => endpoints.MapRazorPages());
        }
    }
}
