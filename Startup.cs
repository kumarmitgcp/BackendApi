using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackendApi.Models;
using BackendApi.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using AutoMapper;
using BackendApi.DBContexts;
using Microsoft.AspNetCore.Cors.Infrastructure;

namespace BackendApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            

            services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                                  builder =>
                                  {
                                      builder.WithOrigins("http://35.184.136.165:8081","http://10.128.0.15:8081");
                                  });
            });

            services.AddControllers(setupAction =>
                {
                    setupAction.ReturnHttpNotAcceptable = true;
                }).AddXmlDataContractSerializerFormatters() ;

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddScoped<IEmployeeRepository, EmployeeRepository>();

            //services.AddDbContext<CoreDbContext>(op => op.UseSqlServer(Configuration.GetConnectionString("Database"))); //Add

            services.AddDbContext<CoreDbContext>(op => op.UseNpgsql(Configuration.GetConnectionString("postgres")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseCors(MyAllowSpecificOrigins);

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
