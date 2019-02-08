using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Picks.core.Entities;
using Picks.infrastructure.Cart;
using Picks.infrastructure.Data;
using Picks.infrastructure.Helpers;
using Picks.infrastructure.Repositories.Implementations;
using Picks.infrastructure.Repositories.Interfaces;
using Picks.infrastructure.Services.Implementations;
using Picks.infrastructure.Services.Interfaces;

namespace Picks.web
{
    public class Startup
    {
        IConfiguration _configuration;

        public Startup(IConfiguration conf)
        {
            _configuration = conf;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var conn = _configuration.GetConnectionString("Picks");

            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(conn));
            
            //services.Configure<CookiePolicyOptions>(options =>
            //{
            //    // This lambda determines whether user consent for non-essential cookies is needed for a given request.
            //    options.CheckConsentNeeded = context => true;
            //    options.MinimumSameSitePolicy = SameSiteMode.None;
            //});
            
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddDistributedRedisCache(opt =>
            {
                opt.Configuration = _configuration.GetConnectionString("Redis");
                //opt.InstanceName = "main_";
            });
            services.AddMemoryCache();

            services.AddSession(opt =>
            {
                opt.Cookie.Name = "Picks.io";
            });

            services.AddScoped(x => CartSession.GetCart(x));
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            
            services.AddTransient<IImageRepository, ImageRepository>();
            services.AddTransient<IImageService, ImageService>();

            services.AddTransient<ICategoryRepository, CategoryRepository>();
            services.AddTransient<ICategoryService, CategoryService>();

            services.AddTransient<CartSession>();

            services.Configure<AzureStorageConfig>(options => _configuration.GetSection("AzureStorageConfig").Bind(options));
            services.AddTransient<UploadImageHelper>();
            services.AddTransient<AzureCognitiveServicesSettings>();
            services.AddAutoMapper();
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
                app.UseHsts();
            }
            
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseSession();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
