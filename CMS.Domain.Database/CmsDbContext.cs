using Microsoft.EntityFrameworkCore;
using CMS.Domain.Data.Security;

namespace CMS.Domain.Database
{
    public class CmsDbContext : DbContext
    {
        private const int DefaultStringLength = 256;

        public CmsDbContext(DbContextOptions<CmsDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public void Seed()
        {

        }
    }
}
