using System.Net;

namespace BuildingBlocks.Domain
{
    public class EntityNotFoundException<TEntity> : HttpRequestException where TEntity : IAggregateRoot
    {
        public EntityNotFoundException() : this(string.Empty)
        {
        }

        public EntityNotFoundException(string message)
            : base($"Entity with type {typeof(TEntity).FullName} could not found{(!string.IsNullOrEmpty(message) ? $" with message: {message}" : string.Empty)}", null, HttpStatusCode.NotFound)
        {
        }
    }
}
