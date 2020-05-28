using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyCompany.MyProject.Database;
using MyCompany.MyProject.DataRepos;
using MyCompany.MyProject.DataRepos.Interface;
using MyCompany.MyProject.Logic;
using MyCompany.MyProject.Logic.Interface;

namespace MyCompany.MyProject.Service
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
            //Get db connecting configs and set this. 
            services.AddDbContext<MyDbContext>(options => { options.UseMySql("Server=serverIp;User Id=id;Password=password;Database=database;TreatTinyAsBoolean=false"); });
            #region DI 
            services.AddScoped<IMyProjectUnitOfWork, MyProjectUnitOfWork>();
            services.AddScoped<IClassLogic, ClassLogic>();
            services.AddScoped<IStudentLogic, StudentLogic>();
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

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
