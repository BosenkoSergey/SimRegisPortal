using Microsoft.EntityFrameworkCore;
using CMS.Domain.Data.Entities;

namespace CMS.Domain.Database
{
    public class CmsDbContext : DbContext
    {
        private const int DefaultStringLength = 256;
        private const int PasswordHashLength = 128;

        public CmsDbContext(DbContextOptions<CmsDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Security

            //modelBuilder.Entity<User>(
            //    e =>
            //    {
            //        e.Property(a => a.Name).HasMaxLength(DefaultStringLength).IsRequired();
            //        e.Property(a => a.Login).HasMaxLength(DefaultStringLength).IsRequired();
            //        e.Property(u => u.PasswordHash).HasMaxLength(PasswordHashLength).IsRequired();
            //        e.Property(a => a.Email).HasMaxLength(DefaultStringLength).IsRequired();
            //    });

            #endregion

            base.OnModelCreating(modelBuilder);
        }

        public void Seed()
        {

        }
    }
}
