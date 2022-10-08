using BuildingBlocks.Domain;

namespace BuildingBlocks.Application.CQRS.Commands
{
    public class EntityResult
    {
        private EntityBase? entity;

        public void SetEntity(EntityBase entity) => this.entity = entity;

        public TEntity? GetEntity<TEntity>() where TEntity : EntityBase => entity as TEntity;
    }
}
