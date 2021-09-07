using CarSales.Common.Mapping;
using CarSales.Data;
using CarSales.Repository.Implementations;
using CarSales.Repository.Interfaces;
using CarSales.Services.Interfaces;
using CarSales.Services.Services;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace CarSales
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
            services.AddDbContext<ApplicationDbContext>(options =>
               options.UseSqlServer(
                   Configuration.GetConnectionString("DefaultConnection")));
            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddControllers().ConfigureApiBehaviorOptions(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            services.AddAutoMapper(typeof(MappingProfile));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CarSales", Version = "v1" });
            });

            services.AddTransient<IVehiclesService, VehiclesService>();
            services.AddTransient<IClientsService, ClientsService>();
            services.AddTransient<IVehiclesRepository, VehiclesRepository>();
            services.AddTransient<IClientsRepository, ClientsRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, AutoMapper.IConfigurationProvider configurationProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CarSales v1"));
            }

            //app.UseExceptionHandler(options =>
            //{
            //    options.Run(async context =>
            //    {
            //        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            //        context.Response.ContentType = "text/html";
            //        var exceptionObject = context.Features.Get<IExceptionHandlerFeature>();
            //        if (null != exceptionObject)
            //        {
            //            var errorMessage = $"{exceptionObject.Error.Message}";
            //            Log.Error(exceptionObject.Error, errorMessage);
            //            await context.Response.WriteAsync(errorMessage);
            //        }
            //    });
            //});

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            configurationProvider.AssertConfigurationIsValid();
        }
    }
}
