using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SimRegisPortal.Core.Entities;

namespace SimRegisPortal.Persistence.Context.Configurations
{
    internal sealed class UserProjectPermissionConfiguration : IEntityTypeConfiguration<UserProjectPermission>
    {
        public void Configure(EntityTypeBuilder<UserProjectPermission> builder)
        {
            builder.HasKey(p => new { p.UserId, p.ProjectId, p.PermissionType });

            builder.HasOne(p => p.User)
                .WithMany(u => u.ProjectPermissions)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(p => p.Project)
                .WithMany()
                .HasForeignKey(p => p.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.ToTable(nameof(UserProjectPermission));
        }
    }
}
