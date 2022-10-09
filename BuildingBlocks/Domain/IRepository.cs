namespace BuildingBlocks.Domain
{
    public interface IRepository<TEntity> where TEntity : class, IAggregateRoot
    {
    }
}
