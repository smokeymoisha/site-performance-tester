using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SitePerformanceTester.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using SitePerformanceTester.DataAccess.Interfaces;
using SitePerformanceTester.DataAccess.Repositories;
using SitePerformanceTester.BusinessLogic.Interfaces;
using SitePerformanceTester.BusinessLogic.Managers;

namespace SitePerformanceTester.MVC
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
            services.AddControllersWithViews();

            string connection = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<TesterContext>(options => options.UseSqlServer(connection));

            services.AddAutoMapper(typeof(Startup));

            services.AddTransient<IRequestRepository, RequestRepository>();
            services.AddTransient<IUrlRepository, UrlRepository>();
            services.AddTransient<IRequestManager, RequestManager>();
            services.AddTransient<ISitemapUrlManager, SitemapUrlManager>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
