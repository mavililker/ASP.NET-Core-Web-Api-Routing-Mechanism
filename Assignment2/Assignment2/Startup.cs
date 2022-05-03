using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }



            app.UseWhen(ctx => ctx.Request.Path == "/api/values/getvalues" || ctx.Request.Path == "/api/values/getvalues2" && ctx.Request.Method == "GET", config => {
                config.Use(async (context, next) =>
                {
                    if (context.Request.Headers.TryGetValue("Key", out StringValues key) && context.Request.Headers.TryGetValue("Name", out StringValues name))
                    {
                        if ((key == "123" && name == "ilker") || ((key == "124" && name == "ozan")))
                        {
                            await next.Invoke();
                        }
                        else
                        {
                            context.Response.StatusCode = 401;
                        }
                    }
                    else
                    {
                        context.Response.StatusCode = 400;
                        context.Response.ContentType = "application/json";
                        byte[] data = Encoding.UTF8.GetBytes("Key or name missing");
                        await context.Response.BodyWriter.WriteAsync(data);

                    }
                });
            });

            app.UseRouting();

            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}
