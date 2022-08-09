using DefaultGenericProject.Core.Models.Users;
using DefaultGenericProject.Core.Services;
using DefaultGenericProject.Core.Services.Users;
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
            if (context.Users.Count() == 0)
            {
                context.Users.Add(new User()
                {
                    Username = "SuperAdmin",
                    Email = "superadmin@izmirteknoloji.com.tr",
                    Password = authService.PasswordHash("Password*-1"),
                });
                if (context.Roles.Count() == 0)
                {
                    context.Roles.AddRange(new List<Role>()
                    {
                    new Role(){ Name = RoleInfo.Admin},
                    new Role(){ Name = RoleInfo.Member}
                    });
                }
                context.SaveChanges();
                var roleId = context.Roles.Where(x => x.Name == RoleInfo.Admin).Select(x => x.Id).FirstOrDefault();
                var userId = context.Users.Select(x => x.Id).FirstOrDefault();
                context.UserRoles.Add(new UserRole()
                {
                    UserId = userId,
                    RoleId = roleId
                });
                context.SaveChanges();
            }
        }
    }
}
