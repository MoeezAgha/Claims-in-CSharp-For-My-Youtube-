using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Claims
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

            // First  services.AddAuthentication().AddCookie("MrCookiesAuth",
            //          options => { options.Cookie.Name = "MrCookiesAuth"; }) ;// add this after error 

            services.AddAuthentication("MrCookiesAuth").AddCookie("MrCookiesAuth",
             options => { options.Cookie.Name = "MrCookiesAuth";
                 options.LoginPath = "/Login";
                 // options.ExpireTimeSpan = TimeSpan.FromMinutes(1);
                  //options.Cookie.Name = "MrCookiesAuth";
                  //options.AccessDeniedPath = "/Login";
             });// add this after error 

            services.AddAuthorization(policy =>
            {
                policy.AddPolicy("MustHaveAgha",
                    policy => policy.RequireClaim("Department", "HR"));
            });
            services.AddRazorPages();
      
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
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication(); //above step 2
            app.UseAuthorization();
       

            

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}
