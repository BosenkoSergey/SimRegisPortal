namespace SimRegisPortal.Domain.Database.UnitOfWork
{
    public class SimRegisUnitOfWork : UnitOfWork
    {
        public SimRegisUnitOfWork(SimRegisDbContext context, IServiceProvider serviceProvider)
            : base(context, serviceProvider)
        {
        }
    }
}
