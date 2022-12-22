using DefaultGenericProject.Core.Configuration;
using DefaultGenericProject.Data;
using DefaultGenericProject.Data.Seeds;
using DefaultGenericProject.Service.Filters;
using DefaultGenericProject.Service.Hubs;
using DefaultGenericProject.Service.Services.Auth;
using DefaultGenericProject.Service.Validations;
using DefaultGenericProject.WebApi.Extensions;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption.ConfigurationModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Authorization;
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
        private readonly string _policyName = "CorsPolicy";

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.ConfigureCors(_policyName);

            services.ConfigureDependencies();

            services.AddHttpContextAccessor();

            services.AddDbContext<AppDbContext>(opts =>
            {
                opts.UseSqlServer(Configuration.GetConnectionString("db"));
            });

            services.AddSignalR();

            services.ConfigureSection(Configuration);

            services.ConfigureAuthentication(Configuration);

            services.AddDataProtection().UseCryptographicAlgorithms(new AuthenticatedEncryptorConfiguration()
            {
                EncryptionAlgorithm = EncryptionAlgorithm.AES_256_GCM,
                ValidationAlgorithm = ValidationAlgorithm.HMACSHA256
            });

            services.AddControllers(x =>
            {
                x.Filters.Add(new AuthorizeFilter());
                x.Filters.Add(new ValidateFilterAttribute());
            }).AddFluentValidation(opts =>
            {
                opts.RegisterValidatorsFromAssemblyContaining<LoginDTOValidator>();
            });

            services.AddCustomValidationResponse();

            services.ConfigureSwagger();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                SeedData.Seed(app.ApplicationServices); // Geliþtirme aþamasýnda seed data oluþturma.
            }
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "DefaultGenericProject.WebApi v1");
                c.RoutePrefix = "docs";
            });

            app.UseStaticFiles();

            app.UseCustomException();

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors(_policyName);
            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<ChatHub>("/chat");
            });
        }
    }
}
