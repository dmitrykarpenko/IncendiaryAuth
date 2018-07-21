using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IncendiaryAuth.Dal.Abstract.Repositories;
using IncendiaryAuth.Dal.Repositories;
using IncendiaryAuth.Domain.Abstract.LogicInterfaces;
using IncendiaryAuth.Domain.LogicImplementations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace IncendiaryAuth.Web
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IAuthRepository, AuthRepository>();
            services.AddTransient<IAuthLogic, AuthLogic>();

            services.AddAntiforgery(options => options.HeaderName = "X-XSRF-Token");
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            app.UseMvc(routeBuilder =>
            {
                routeBuilder.MapRoute("Default", "{controller=Auth}/{action=User}");
            });

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Not found");
            });
        }
    }
}
