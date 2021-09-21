using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MWS_SocialNetwork.Data;
using MWS_SocialNetwork.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using MWS_SocialNetwork.Services;
using NToastNotify;
using MWS_SocialNetwork.ViewModels;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace MWS_SocialNetwork
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
            
            //services.ConfigureApplicationCookie(options =>
            //{
            //    options.Cookie.Name = "auth_cookie";
            //    options.Cookie.SameSite = SameSiteMode.None;
            //    options.LoginPath = new PathString("/Home/Index");
            //    options.AccessDeniedPath = new PathString("/Home/Index");
            //    options.Events.OnRedirectToLogin = async (context) =>
            //    {
            //        context.HttpContext.Response.Redirect("http://www.google.com");
            //    };
            //    options.Events.OnRedirectToAccessDenied = context =>
            //    {
            //        context.Response.StatusCode = StatusCodes.Status403Forbidden ;
            //        return Task.CompletedTask;
            //    };
            //});

            //        services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            //.AddCookie(o =>
            //{
            //    o.Cookie.Name = "myCookie";
            //    o.Events = new CookieAuthenticationEvents()
            //    {
            //        OnRedirectToLogin = (context) =>
            //        {
            //            context.HttpContext.Response.Redirect("http://www.google.com");
            //            return Task.CompletedTask;
            //        }
            //    };
            //});

            


            var connection = Configuration.GetConnectionString("DatabaseConnection");


            services.AddDbContext<DatabaseContext>(options => options.UseSqlServer(connection), ServiceLifetime.Transient);
            

            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequireNonAlphanumeric = false;
            })
           .AddEntityFrameworkStores<DatabaseContext>();

            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = new PathString("/Home/Index");
                options.AccessDeniedPath = new PathString("/Home/Index");
            });




            //services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddMvc().AddNToastNotifyToastr(new ToastrOptions()
            {
                ProgressBar = false,
                PositionClass = ToastPositions.BottomRight
            });

            services.AddScoped<IProfileService, ProfileService>();
            services.AddScoped<ISharedService, SharedService>();
            services.AddScoped<IContactService, ContactService>();
            services.AddScoped<IGroupService, GroupService>();
            services.AddScoped<ISettingsService, SettingsService>();
            services.AddScoped<IAddPostService, AddPostService>();
            services.AddScoped<IViewPostService, ViewPostService>();
            services.AddScoped<INotificationService, NotificationService>();
            services.AddScoped<IAdminService, AdminService>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();


            //Admin Page Configuration
            services.Configure<AdminLogin>(Configuration.GetSection("AdminLogin"));


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseAuthentication();
            //app.UseCookiePolicy();
            app.UseNToastNotify();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
