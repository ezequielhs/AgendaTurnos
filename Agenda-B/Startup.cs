using Agenda_B.Data;
using Agenda_B.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Agenda_B
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


            //services.AddDbContext<AgendaContext>(options => options.UseInMemoryDatabase("AgendaDb"));
            services.AddDbContext<AgendaContext>(options => options.UseSqlServer(Configuration.GetConnectionString("Agenda")));

            services.AddIdentity<Persona, IdentityRole<int>>().AddEntityFrameworkStores<AgendaContext>();
            
            //TODO: sacar esto?
            services.Configure<IdentityOptions>(
                opciones =>
                {
                    opciones.Password.RequiredLength = 5;
                }
            );

            services.PostConfigure<CookieAuthenticationOptions>(IdentityConstants.ApplicationScheme,
                opciones =>
                {
                    opciones.LoginPath = "/Cuenta/IniciarSesion";
                    opciones.AccessDeniedPath = "/Cuenta/AccesoDenegado";
                }
            );

            services.AddScoped<Seeder>();
            

            services.AddControllersWithViews();
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

            using (var servicescope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                servicescope.ServiceProvider.GetService<Seeder>().Seed();
            }

                app.UseRouting();

            app.UseAuthentication();
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
