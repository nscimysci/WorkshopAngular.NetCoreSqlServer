using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APPAPI.Database;
using APPAPI.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace APPAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        readonly string AllowMyOrigin = "AllowMyOrigin";

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            // ให้สามารถ Cross-Origin Resource Sharing (CORS)
            services.ConfigureCors();

            // ให้อ่าน Config Sql Server และ AddDbContext
            services.ConfigureSqlContext(Configuration);

            // อ่าน Token JWT
            services.ConfigureJWTuthentication(Configuration);


            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            // ใช้งาน CORS
            app.UseCors(AllowMyOrigin);

            app.UseRouting();

            // ใช้งาน Authen
            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
