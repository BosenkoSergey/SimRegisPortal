namespace SimRegisPortal.Persistence.UnitOfWork
{
    public class SimRegisUnitOfWork : UnitOfWork
    {
        public SimRegisUnitOfWork(SimRegisDbContext context, IServiceProvider serviceProvider)
            : base(context, serviceProvider)
        {
        }
    }
}
