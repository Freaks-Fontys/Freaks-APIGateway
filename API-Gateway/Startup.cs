using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_Gateway
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //TODO: Creeer een complete private key die niemand weet
            //
            var secret = "8Wti9dgJsps67_bDuTzDdCMo7sVQ7pw9_zKIX4l5Huy_tNlXMnW7lCUbqvSk4YCJQygqu4tX4LjKXCI33NHujEbfE4EXZkcdyPvFyvTKaSnMwCsX9wHcN-Hghm5DSVBYU1mIQPwFjdX6vJI03TWhNEaHI1dHhgpP9t--Ko-Chn8";
            var key = Encoding.ASCII.GetBytes(secret);

            services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {

                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = false,
                    ValidateAudience = false

                };
            });
            //services.AddCors(options =>
            //{
            //    options.AddPolicy(name: "dev",
            //        builder =>
            //        {
            //            builder.WithOrigins("http://localhost:4200").AllowAnyMethod();
            //        });
            //});

            services.AddCors();
            services.AddOcelot();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello World!");
                });
            });

            app.UseOcelot().Wait();
        }
    }
}
