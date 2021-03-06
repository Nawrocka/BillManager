using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using BillManager.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using BillManager.Models;
using Microsoft.OpenApi.Models;
using AutoMapper;
using BillManager.Extensions.Mapper;
using Microsoft.AspNetCore.Http;
using BillManager.Services.Interfaces;
using BillManager.Services.Implementations;
using Microsoft.AspNetCore.Mvc;

namespace BillManager
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
            services.AddCors();
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed
                // for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddAutoMapper(typeof(MappingProfile));
            // Add application services.
            services.AddTransient<IUsersService, UsersService>();
            services.AddTransient<IBillsService, BillsService>();
            services.AddLogging();
            services.AddTransient<IInformationsService, InformationsService>();
            services.AddMvc(config => 
                config.EnableEndpointRouting = false)
                    .SetCompatibilityVersion(CompatibilityVersion.Version_3_0);            

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Bill Manager"
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseAuthentication();

            app.UseCors(builder => builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
            //.AllowCredentials());

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                name: "default",
                template: "{controller=Home}/{action=Index}/{id?}");
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Bill Manager");
            });
        }


    }
}

            //public class Startup
            //{
            //    public Startup(IConfiguration configuration)
            //    {
            //        Configuration = configuration;
            //    }

            //    public IConfiguration Configuration { get; }

            //    // This method gets called by the runtime. Use this method to add services to the container.
            //    public void ConfigureServices(IServiceCollection services)
            //    {
            //        services.AddDbContext<ApplicationDbContext>(options =>
            //            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            //        //services.AddDefaultIdentity<Identithttps://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/yUser>(options => options.SignIn.RequireConfirmedAccount = true)
            //        //    .AddEntityFrameworkStores<ApplicationDbContext>();


            //        services.AddIdentity<ApplicationUser, IdentityRole>(config=>
            //        {
            //            config.SignIn.RequireConfirmedAccount = false;
            //            config.Password.RequiredLength = 4;
            //            config.Password.RequireDigit = false;
            //            config.Password.RequireNonAlphanumeric = false;
            //            config.Password.RequireUppercase = false;
            //        })
            //            .AddEntityFrameworkStores<ApplicationDbContext>()
            //            .AddDefaultTokenProviders();

            //        //services.ConfigureApplicationCookie(config =>
            //        //{
            //        //    config.Cookie.Name = "Identity.Cookie";
            //        //    config.LoginPath = "/Register/Login";
            //        //});

            //        services.Configure<CookiePolicyOptions>(options =>
            //        {
            //            options.CheckConsentNeeded = context => true;
            //            options.MinimumSameSitePolicy = SameSiteMode.None;
            //        });

            //        services.AddTransient<IBillsService, BillsService>();
            //        services.AddTransient<IInformationsService, InformationsService>();
            //        services.AddTransient<IUsersService, UsersService>();
            //        services.AddLogging();
            //        services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            //        services.AddSwaggerGen(c =>
            //        {
            //            c.SwaggerDoc("v1", new OpenApiInfo
            //            {
            //                Version = "v1",
            //                Title = "Bill Manager"
            //            });
            //        });

            //        //var config = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
            //        //var mapper = new Mapper(config);
            //        //services.AddAutoMapper();
            //        //services.AddControllersWithViews();
            //        services.AddAutoMapper(typeof(MappingProfile));
            //    }

            //    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
            //    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
            //    {     
            //        if (env.IsDevelopment())
            //        {
            //            app.UseDeveloperExceptionPage();
            //            app.UseDatabaseErrorPage();
            //        }
            //        else
            //        {
            //            app.UseExceptionHandler("/Home/Error");
            //            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            //            app.UseHsts();
            //        }
            //        app.UseHttpsRedirection();
            //        app.UseStaticFiles();
            //        app.UseCookiePolicy();

            //        app.UseRouting();

            //        app.UseAuthentication();
            //        app.UseAuthorization();

            //        app.UseCors(builder => builder
            //        .AllowAnyOrigin()
            //        .AllowAnyMethod()
            //        .AllowAnyHeader());
            //        //.AllowCredentials());

            //        app.UseEndpoints(endpoints =>
            //        {
            //            endpoints.MapControllerRoute(
            //                name: "default",
            //                pattern: "{controller=Home}/{action=Index}/{id?}");
            //            endpoints.MapRazorPages();
            //        });

            //        app.UseSwagger();
            //        app.UseSwaggerUI(c =>
            //        {
            //            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Bill Manager");
            //        });
            //    }
            //}
        
