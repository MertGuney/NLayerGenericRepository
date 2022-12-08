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
using Microsoft.Extensions.DependencyInjection;

namespace DefaultGenericProject.WebApi.Extensions
{
    public static class CustomExtension
    {
        public static void AddDependencies(this IServiceCollection services)
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
    }
}
