using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Daimler.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Daimler
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
             string allowSpecificOrigins = "_allowSpecificOrigins";
            string connectionString = Configuration.GetConnectionString("default");
            services.AddAntiforgery(o => o.HeaderName = "XSRF-TOKEN");

            // services.AddDbContext<DaimlerContext>(c => c.UseSqlServer(connectionString));
            // services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<DaimlerContext>();
            services.AddMvc().AddRazorPagesOptions(options =>
            {
                options.Conventions.AddPageRoute("/Login", "");
            });
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(120);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });
            services.AddCors(options =>

            {

                options.AddPolicy(allowSpecificOrigins,

                builder =>

                {

                    builder.WithOrigins("https://localhost:44372")

                            .AllowAnyHeader()

                            .AllowAnyMethod();

                });

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
            }
            app.UseCors();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();
            app.UseAuthentication();
            app.UseSession();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}
