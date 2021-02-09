namespace Accounts.Domain.SeedWork
{
    public interface IRepository<TAggregateModel> where TAggregateModel : IAggregateRoot
    {
        IUnitOfWork UnitOfWork { get; }
    }
}