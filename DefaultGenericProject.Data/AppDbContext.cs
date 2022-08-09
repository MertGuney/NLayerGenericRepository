using DefaultGenericProject.Core.Models;
using DefaultGenericProject.Core.Models.Users;
using Microsoft.EntityFrameworkCore;

namespace DefaultGenericProject.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Product> Products { get; set; }

        #region UsersDbSet
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<UserRefreshToken> UserRefreshTokens { get; set; }
        #endregion
        #region Logs
        public DbSet<NLog> NLogs { get; set; }
        #endregion
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(GetType().Assembly);

            base.OnModelCreating(builder);
        }
    }
}