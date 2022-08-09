using DefaultGenericProject.Core.Models.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DefaultGenericProject.Data.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name).HasMaxLength(32);
            builder.Property(x => x.Surname).HasMaxLength(32);
            builder.Property(x => x.FullName).HasMaxLength(64);
            builder.Property(x => x.Username).HasMaxLength(64);
            builder.Property(x => x.Email).HasMaxLength(128).IsRequired();
            builder.Property(x => x.Password).HasMaxLength(256);
            builder.Property(x => x.PhotoUrl).HasMaxLength(512);
            builder.Property(x => x.Address).HasMaxLength(512);
            builder.Property(x => x.PhoneNumber).HasMaxLength(11);
            builder.Property(x => x.IpAddress).HasMaxLength(64);

            builder.HasIndex(x => x.Username).IsUnique();
            builder.HasIndex(x => x.Email).IsUnique();

            builder.HasMany(x => x.UserRoles).WithOne(x => x.User).HasForeignKey(x => x.UserId);
        }
    }
}