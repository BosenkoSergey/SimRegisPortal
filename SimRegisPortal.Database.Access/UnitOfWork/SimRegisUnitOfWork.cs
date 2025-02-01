namespace SimRegisPortal.Database.Access.UnitOfWork
{
    public class SimRegisUnitOfWork : UnitOfWork
    {
        public SimRegisUnitOfWork(SimRegisDbContext context, IServiceProvider serviceProvider)
            : base(context, serviceProvider)
        {
        }
    }
}
