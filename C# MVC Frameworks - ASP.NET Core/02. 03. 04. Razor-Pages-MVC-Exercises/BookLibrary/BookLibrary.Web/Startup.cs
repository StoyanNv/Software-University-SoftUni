using System.Diagnostics;
using BookLibrary.Web.Filters;
using Serilog;
using Serilog.Core;

namespace BookLibrary.Web
{
    using Data;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

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
                options.CheckConsentNeeded = context => false;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            string connectionString = this.Configuration.GetConnectionString("BookLibrary");
            services.AddDbContext<BookLibraryContext>(
                options => options.UseSqlServer(connectionString, b => b.MigrationsAssembly("BookLibrary.Web")))
                .AddMvc(options =>
                {
                    options.Filters.Add<LogExecution>();
                    options.Filters.Add<ExceptionFilter>();
                });
        
            services.AddSession(options => options.Cookie.IsEssential = true);
            services.AddScoped<LoggerConfiguration>();
            services.AddScoped<Stopwatch>();
            services.AddDistributedMemoryCache();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
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
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseSession();
            app.UseMvcWithDefaultRoute();
        }
    }
}