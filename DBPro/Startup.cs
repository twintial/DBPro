using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DBPro.Database;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DBPro
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            
            
            // Enable CORS with Atrribute EnableCors("MyPolicy") ***
            services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));

           // services.AddTransient<IConnectionFactory, ConnectionFactory>();
            //services.AddSingleton<IConfiguration>(Configuration);


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();
            //services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
             //   .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options => {
             //       options.AccessDeniedPath = "/Account/Forbidden/";
             //       options.LoginPath = "/Sign/SignIn/";
             //   });
            /*
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration["Data:DefaultConnection:ConnectionString"]));

            services.AddIdentity<ApplicationUser, IdentityRole>(options => {
                options.Cookies.ApplicationCookie.AuthenticationScheme = "ApplicationCookie";
                options.Cookies.ApplicationCookie.CookieName = "Interop";
            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();*/
        

    }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {


            //Apply Mypolicy to all methods ***
            app.UseCors("MyPolicy");

            //Apply Authentication ****
            app.UseAuthentication();

            


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes
               .MapRoute(
                   name: "signin",
                    template: "{controller=Sign}/{action=SignInHandle}/{id?}")
               .MapRoute(
                   name: "signup",
                    template: "{controller=Sign}/{action=SignUpHandle}/{id?}")
                .MapRoute(
                   name: "update",
                    template: "{controller=Account}/{action=UpdateHandle}/{id?}")
                .MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
