using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using BartenderApp.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Session;
using Microsoft.AspNetCore.Identity;

namespace BartenderApp
{
    public class Startup
    {
        IConfigurationRoot Configuration;
        public Startup(IWebHostEnvironment env)
        {
            Configuration = new ConfigurationBuilder()
            .SetBasePath(env.ContentRootPath)
            .AddJsonFile("appsettings.json").Build();
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddTransient<ICocktailRepository, EFCocktailRepository>();
            services.AddScoped<Cart>(sp => SessionCart.GetCart(sp));//specifies that the same object should be used to satisfy related requests for Cart instances. How requests are related can be configured, but by default, it means that any Cart required by components handling the same HTTP request will receive the same object.
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();//specifies that the same object should always be used. The service we created tells MVC to use the HttpContextAccessor class when implementations of the IHttpContextAccessor interface are required.
            services.AddTransient<IOrderRepository, EFOrderRepository>();
            services.AddIdentity<AppUser, IdentityRole<Guid>>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();
            //The AddMvc method that we call here is an extension method 
            //it sets up the shared objects used in MVC applications
            services.AddMvc(options => options.EnableEndpointRouting = false)
                .AddNewtonsoftJson();//Enabling sessions requires adding services and middleware
            services.AddMemoryCache();//sets up the in-memory data store.
            services.AddSession();//registers the services used to access session data
            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage(); //This extension method displays details of exceptions that occur in the application, which is useful during the development process.
                app.UseStatusCodePages(); //This extension method adds a simple message to HTTP responses that would not otherwise have a body, such as 404 - Not Found responses.
                app.UseStaticFiles(); //This extension method enables support for serving static content from the wwwroot folder.
                app.UseAuthentication();
                app.UseSession();//allows the session system to automatically associate requests with sessions when they arrive from the client.
                app.UseMvc(routes => {
                    routes.MapRoute(//this is changing the URL to display "cocktails/page#" from pages > 1
                        name: "pagination",
                        template: "Cocktails/Page{page}",
                        defaults: new { Controller = "Cocktail", action = "List" });
                    routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

                });
            }
        }
    }
}
