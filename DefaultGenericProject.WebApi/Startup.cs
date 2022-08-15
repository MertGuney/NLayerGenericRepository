using DefaultGenericProject.Core.Configuration;
using DefaultGenericProject.Core.DTOs.Tokens;
using DefaultGenericProject.Data;
using DefaultGenericProject.Data.Seeds;
using DefaultGenericProject.Service.Services;
using DefaultGenericProject.Service.Validations;
using DefaultGenericProject.WebApi.Extensions;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;

namespace DefaultGenericProject.WebApi
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
            services.AddDependencies();

            services.AddHttpContextAccessor();

            services.AddDbContext<AppDbContext>(opts =>
            {
                opts.UseSqlServer(Configuration.GetConnectionString("LocalDb"));
                //opts.UseSqlServer(Configuration.GetConnectionString("DefaultDb"));
            });

            services.Configure<List<Client>>(Configuration.GetSection("Clients"));
            services.Configure<CustomTokenOption>(Configuration.GetSection("TokenOption"));

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, opts =>
            {
                var tokenOptions = Configuration.GetSection("TokenOption").Get<CustomTokenOption>();
                opts.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidIssuer = tokenOptions.Issuer,
                    ValidAudience = tokenOptions.Audience[0],
                    IssuerSigningKey = SignService.GetSymmetricSecurityKey(tokenOptions.SecurityKey),
                    ValidateIssuerSigningKey = true,
                    ValidateAudience = true,
                    ValidateIssuer = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });

            services.AddControllers().AddFluentValidation(opts =>
            {
                opts.RegisterValidatorsFromAssemblyContaining<LoginDTOValidator>();
            });

            services.AddCustomValidationResponse();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "DefaultGenericProject.WebApi", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "DefaultGenericProject.WebApi v1");
                    c.RoutePrefix = "docs";
                });
                SeedData.Seed(app.ApplicationServices); // Geliþtirme aþamasýnda seed data oluþturma.
            }

            app.UseCustomException();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
