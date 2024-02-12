using System;
using AutoMapper;
using group8my.Models;
using group8my.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace group8my
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

            services.AddControllers(o =>
            {
                o.FormatterMappings.SetMediaTypeMappingForFormat("xml", "application/xml");
                o.FormatterMappings.SetMediaTypeMappingForFormat("js", "application/json");

            }).AddXmlSerializerFormatters();

            services.AddDbContext<ManyJobsContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("Connection2ManyJobsDB")));
            // paraeter store 
            /*var bulider = new SqlConnectionStringBuilder(Configuration.GetConnectionString("Connection2ManyJobsDB"));
            bulider.UserID = Configuration["Dbuser"];
            bulider.Password = Configuration["DbPassword"];
            var connection = bulider.ConnectionString;
            services.AddDbContext<ManyJobsContext>(options => options.UseSqlServer(connection));*/

            services.AddDbContext<ManyJobsContext>(options => options.UseSqlServer(Configuration.GetConnectionString("Connection2ManyJobsDB")));

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddScoped<IJobOfferRepository, JobOfferRepository>();
            services.AddScoped<IJobSeekerRepository, JobSeekerRepository>();

            services.AddMvcCore().AddApiExplorer();

            services.AddSwaggerGen();
            services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo { Title = "ManyJobs API", Version = "v1" }); });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();

            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "Many Jobs V1"); });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
