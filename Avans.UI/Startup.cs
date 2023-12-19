using Avans.APIConnection;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Avans.UI
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
            services.AddHttpClient<AdvanceService>(conf =>
            {
                conf.BaseAddress = new Uri(Configuration["MyBaseUri"]);
            });
            services.AddHttpClient<ProjectService>(conf =>
            {
                conf.BaseAddress = new Uri(Configuration["MyBaseUri"]);
            });
            services.AddHttpClient<ApiRequestService>(conf =>
            {
                conf.BaseAddress = new Uri(Configuration["MyBaseUri"]);
            });
            services.AddHttpClient<TitleService>(conf =>
            {
                conf.BaseAddress = new Uri(Configuration["MyBaseUri"]);
            });

            services.AddAuthentication(a =>
            {
                a.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                a.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                a.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            }).AddCookie(a =>
            {
                a.LoginPath = "/Account/Login";
                a.Cookie.Name = CookieAuthenticationDefaults.AuthenticationScheme;
                a.Cookie.HttpOnly = true;
            });

            services.AddAuthorization();
            services.AddMemoryCache();


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
            }
            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();

            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                   name: "areas",
                   pattern: "{area:exists}/{controller=Register}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                   name: "areas",
                   pattern: "{area:exists}/{controller=Login}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Account}/{action=Login}/{id?}");
            });
        }
    }
}
