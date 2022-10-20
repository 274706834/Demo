using DemoEntity;
using DemoRepository;
using DemoServices;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace MachineDemoApi
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
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("V1", new OpenApiInfo
                {
                    Version = "V1",
                    Title = $" ½Ó¿ÚÎÄµµ",
                    Description = $"HTTP API V1"
                });
                c.OrderActionsBy(o => o.RelativePath);
            });
            services.AddSingleton<IHostedService, WebsocketService>();
            var migrationsAssembly = typeof(DemoDbContext).GetTypeInfo().Assembly.GetName().Name;
            services.AddDbContext<DemoDbContext>(
                options => options.UseSqlServer(Configuration.GetConnectionString("DemoDb"), sql => sql.MigrationsAssembly(migrationsAssembly)));
            //services.AddDbContext<DemoDbContext>(options =>options.UseSqlServer(Configuration.GetConnectionString("DemoDb")));
            services.AddScoped<IMachinesService, MachinesService>();
            services.AddScoped<IMachinesRepository, MachinesRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint($"/swagger/V1/swagger.json", $"HTTP API V1");

                c.RoutePrefix = "";
            });
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
