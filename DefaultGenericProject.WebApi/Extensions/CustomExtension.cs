using DefaultGenericProject.Core.Configuration;
using DefaultGenericProject.Core.Repositories;
using DefaultGenericProject.Core.Repositories.Users;
using DefaultGenericProject.Core.Services;
using DefaultGenericProject.Core.Services.Users;
using DefaultGenericProject.Core.UnitOfWorks;
using DefaultGenericProject.Data.Repositories;
using DefaultGenericProject.Data.Repositories.Users;
using DefaultGenericProject.Data.UnitOfWorks;
using DefaultGenericProject.Service.Services;
using DefaultGenericProject.Service.Services.Auth;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;

namespace DefaultGenericProject.WebApi.Extensions
{
    public static class CustomExtension
    {
        public static void ConfigureDependencies(this IServiceCollection services)
        {
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            services.AddScoped<ICipherService, CipherService>();

            #region Tokens
            services.AddScoped<ITokenService, TokenService>();
            #endregion

            #region Users
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserRepository, UserRepository>();
            #endregion

            #region Generic
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped(typeof(IGenericService<>), typeof(GenericService<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            #endregion
        }
        public static void ConfigureHttpClient(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpClient<IClientCredentialTokenService, ClientCredentialTokenService>();
        }
        public static void ConfigureCors(this IServiceCollection services, string policyName)
        {
            services.AddCors(opts =>
            {
                opts.AddPolicy(policyName, builder =>
                {
                    builder.WithOrigins("http://127.0.0.1:4000", "http://localhost:3000").AllowAnyHeader().AllowAnyMethod().AllowCredentials();
                });
            });
        }
        public static void ConfigureSection(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<List<Client>>(configuration.GetSection("Clients"));
            services.Configure<CustomTokenOption>(configuration.GetSection("TokenOption"));
        }
        public static void ConfigureAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, opts =>
            {
                var tokenOptions = configuration.GetSection("TokenOption").Get<CustomTokenOption>();
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
        }
        public static void ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "DefaultGenericProject.WebApi", Version = "v1" });
                c.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = JwtBearerDefaults.AuthenticationScheme
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = JwtBearerDefaults.AuthenticationScheme
                            }
                        },
                        new List<string>()
                    }
                });
            });
        }
    }
}
