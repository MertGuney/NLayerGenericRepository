using DefaultGenericProject.Core.Models.Users;
using DefaultGenericProject.Core.Services;
using DefaultGenericProject.Core.StringInfos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DefaultGenericProject.Data.Seeds
{
    public static class SeedData
    {
        public static void Seed(IServiceProvider app)
        {
            var scope = app.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            var authService = scope.ServiceProvider.GetRequiredService<IAuthenticationService>();
            context.Database.Migrate();
            if (!context.Users.Any() && !context.Roles.Any())
            {
                User user = new()
                {
                    Username = "SuperAdmin",
                    Email = "superadmin@izmirteknoloji.com.tr",
                    Password = authService.PasswordHash("Password*-1"),
                };
                context.Users.Add(user);

                List<Role> roles = new()
                {
                    new Role(){Name = RoleInfo.Admin },
                    new Role(){Name = RoleInfo.Member }
                };
                context.Roles.AddRange(roles);

                UserRole userRole = new()
                {
                    UserId = roles.Where(x => x.Name == RoleInfo.Admin).Select(x => x.Id).FirstOrDefault(),
                    RoleId = user.Id
                };
                context.UserRoles.Add(userRole);

                context.SaveChanges();
            }
        }
    }
}
