using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebApiMimic.Database;
using WebApiMimic.V1.Repositories.Contracts;
using WebApiMimic.V1.Repositories;
using AutoMapper;
using WebApiMimic.Helpers;

namespace WebApiMimic
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //adicao e configuracao do automapper
            #region AutoMapper Config
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new DTOMapperProfile());
            });
            IMapper mapper = config.CreateMapper();
            services.AddSingleton(mapper);
            #endregion

            services.AddDbContext<MimicContext>(opt =>
            {
                opt.UseSqlite("Data Source=Database\\Mimic.db");
            });
            services.AddMvc(options => options.EnableEndpointRouting = false);
            services.AddOptions();

            services.AddScoped<IPalavraRepository,PalavraRepository>();
            services.AddApiVersioning(cfg =>
            {
                cfg.ReportApiVersions = true;
               // cfg.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseStatusCodePages();
            app.UseMvc();        
        }
    }
}
