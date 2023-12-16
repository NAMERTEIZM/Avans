using Avans.BLL.Concrete;
using Avans.Core.Contexts;
using Avans.Core.Mappers;
using Avans.DAL.Abstract;
using Avans.DAL.Concrete;
using Avans.DTOs;
using Avans.Models.Entities;
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
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Avans.API
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
            //ui için izin ver
            //services.AddCors(opt =>
            //{
            //    opt.AddPolicy("AllowOrigin", a => a.WithOrigins("http://localhost:54825").AllowAnyHeader().AllowAnyMethod());
            //});

            string connectionString = Configuration.GetConnectionString("myconn");
            services.AddScoped<IDbConnection>(conn => new SqlConnection(connectionString));
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped(typeof(IGenericRepository<Advance>), typeof(GenericRepository<Advance>));
            services.AddScoped(typeof(IGenericRepository<Project>), typeof(GenericRepository<Project>));
            services.AddScoped<MyMapper<Project, ProjectSelectDTO>>();

            services.AddScoped<AdvanceService>();
            services.AddScoped<ConnectionHelper>();
            services.AddScoped<AdvanceRepository>();
            services.AddScoped<ProjectRepository>();
            services.AddScoped<ProjectService>();




            services.AddAutoMapper(typeof(Startup));
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Avans.API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Avans.API v1"));
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
