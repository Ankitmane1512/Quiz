﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace QuizBackend
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
            services.AddCors(options => options.AddPolicy("Cors", builder =>
              builder.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader()              
            ));
            services.AddDbContext<QuizContext>(opt => opt.UseInMemoryDatabase("Quiz"));
            services.AddDbContext<UserDbContext>(opt => opt.UseInMemoryDatabase("user"));

            //Seeting Up Identity Framwework 
            services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<UserDbContext>();

            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("this is seceret that you should know"));
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(cfg =>
           {
               cfg.RequireHttpsMetadata = false;
               cfg.SaveToken = true;
               cfg.TokenValidationParameters = new TokenValidationParameters()
               {
                   IssuerSigningKey = signingKey,
                   ValidateIssuer = false,
                   ValidateAudience = false,
                   ValidateLifetime = false,
                   ValidateIssuerSigningKey = true
               };
           });
            

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseAuthentication();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors("Cors");
            app.UseMvc();
        }
    }
}
