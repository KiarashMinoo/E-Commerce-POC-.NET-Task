using BuildingBlocks.Domain;

namespace Domain.Customers
{
    public class Customer : EntityBase, IAggregateRoot
    {
        public Guid Id { get; }
        public string FullName { get; } = null!;
        public string EMail { get; } = null!;
        public string Cell { get; } = null!;

        private Customer() => Id = Guid.NewGuid();

        public Customer(Guid id, string fullName, string eMail, string cell) : this()
        {
            Id = id;
            FullName = fullName;
            EMail = eMail;
            Cell = cell;
        }

        public Customer(string fullName, string eMail, string cell) : this(Guid.NewGuid(), fullName, eMail, cell)
        {
        }
    }
}
