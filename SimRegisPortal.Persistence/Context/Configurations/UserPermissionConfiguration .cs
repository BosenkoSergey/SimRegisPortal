using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SimRegisPortal.Domain.Entities;

namespace SimRegisPortal.Persistence.Context.Configurations
{
    internal sealed class UserPermissionConfiguration : IEntityTypeConfiguration<UserPermission>
    {
        public void Configure(EntityTypeBuilder<UserPermission> builder)
        {
            builder.HasKey(p => new { p.UserId, p.PermissionType });

            builder.HasOne(p => p.User)
                .WithMany(u => u.Permissions)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.ToTable(nameof(UserPermission));
        }
    }
}
