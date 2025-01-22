namespace CMS.Domain.Database.UnitOfWork
{
    public class CmsUnitOfWork : UnitOfWork
    {
        public CmsUnitOfWork(CmsDbContext context, IServiceProvider serviceProvider)
            : base(context, serviceProvider)
        {
        }
    }
}
