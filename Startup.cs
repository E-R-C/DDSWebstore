using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using DDSWebstore.Models;
using Microsoft.EntityFrameworkCore;


namespace DDSWebstore
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
            // services.AddEntityFrameworkSqlite();
            services.AddDbContext<MyDBContext>(options =>
                options.UseSqlite(Configuration.GetConnectionString("MyDBContext")));
                // options.UseSqlite($"Data Source={basepath}/ItemsAndOrders.db"));

            services.AddMvc();
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
            }

            app.UseStaticFiles();
            app.UseMvc();

            // app.UseMvc(routes => {
            //     // routes.MapRoute (
            //     //     name : "default",
            //     //     template : "{controller=Cookies}/{action=Index}/{id?}");
            // });
        }
    }
}
