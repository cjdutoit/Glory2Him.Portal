// --------------------------------------------------------------------------------
// Copyright (c) Christo du Toit. All rights reserved.
// Licensed under the MIT License.
// See License.txt in the project root for license information.
// FREE TO USE TO HELP SHARE THE GOSPEL
// Mark 16:15 NIV "Go into all the world and preach the gospel to all creation."
// https://mark.bible/mark-16-15 
// --------------------------------------------------------------------------------

using G2H.Portal.Web.Brokers.Apis;
using G2H.Portal.Web.Brokers.Loggings;
using G2H.Portal.Web.Foundations.Posts;
using G2H.Portal.Web.Services.Views.PostViews;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Syncfusion.Blazor;
using Syncfusion.Licensing;

namespace G2H.Portal.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration) =>
            Configuration = configuration;

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddSyncfusionBlazor();
            AddRootDirectory(services);
            services.AddLogging();
            services.AddHttpClient();
            AddBrokers(services);
            AddServices(services);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            string syncFustionLicenseKey = Configuration["Syncfusion:LicenseKey"];
            SyncfusionLicenseProvider.RegisterLicense(syncFustionLicenseKey);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }

        private static void AddRootDirectory(IServiceCollection services)
        {
            services.AddRazorPages(options =>
            {
                options.RootDirectory = "/Views/Pages";
            });
        }

        private static void AddBrokers(IServiceCollection services)
        {
            services.AddScoped<IApiBroker, ApiBroker>();
            services.AddScoped<ILoggingBroker, LoggingBroker>();
        }

        private static void AddServices(IServiceCollection services)
        {
            services.AddScoped<IPostService, PostService>();
            services.AddScoped<IPostViewService, PostViewService>();
        }
    }
}