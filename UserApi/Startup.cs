using AutoMapper;
using BLL.Abstract;
using BLL.Implementation;
using Common.Mapper;
using DAL.Abstract;
using DAL.DataAccess;
using DAL.Implmentation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using UserApi.Middleware;

namespace UserApi
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
            #region DbContext

            services.AddDbContext<UserContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("Default"));
            });

            #endregion

            #region Repsoitories

            services.AddScoped<IUserRepository, UserRespoistory>();

            #endregion

            #region Services

            services.AddScoped<IUserService, UserService>();

            #endregion

            #region Mapper

            services.AddAutoMapper(typeof(UserProfile));

            #endregion

            #region Swagger

            services.AddSwaggerGen();

            #endregion

            #region Controllers

            services.AddControllers();

            #endregion

            #region MVC

            services.AddMvc(o =>
            {
                o.OutputFormatters.RemoveType(typeof(HttpNoContentOutputFormatter));
                o.OutputFormatters.Insert(0, new HttpNoContentOutputFormatter
                {
                    TreatNullValueAsNoContent = false
                });
            });

            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseMiddleware(typeof(ExceptionHandlerMiddleWare));

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

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