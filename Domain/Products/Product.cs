using BuildingBlocks.Domain;
using Domain.Products.BusinessRules;
using Domain.Products.BusinessRules.ProductNameMustBeUnique;
using Domain.Products.DomainEvents;

namespace Domain.Products
{
    public class Product : EntityBase, IAggregateRoot
    {
        public Guid Id { get; private set; }

        public string Name { get; private set; } = null!;

        public int Quantity { get; private set; }

        public decimal Price { get; private set; }

        //Used For Ef
        private Product() => Id = Guid.NewGuid();

        public Product(string name, int quantity, decimal price, IProductNameMustBeUniqueHandler nameHandler) : this()
        {
            CheckBusinessRule(new ProductNameMustBeUniqueBusinessRule(nameHandler, Id, name));

            Name = name;
            Quantity = quantity;
            Price = price;
        }

        public Product Update(string name, int quantity, decimal price, IProductNameMustBeUniqueHandler nameHandler)
        {
            CheckBusinessRule(new ProductNameMustBeUniqueBusinessRule(nameHandler, Id, name));

            Name = name;
            Quantity = quantity;
            Price = price;

            return this;
        }

        public Product IncreaseQuantity(int valueToAdded)
        {
            Quantity += valueToAdded;
            return this;
        }

        public Product DecreaseQuantity(int valueToSubtract)
        {
            CheckBusinessRule(new ProductQuantityDecreasingQuantityLessThanZeroBusinessRule(this, valueToSubtract));

            Quantity -= valueToSubtract;

            return this;
        }

        public Product ProductAddedEvent()
        {
            AddDomainEvent(new ProductAddedDomainEvent(this));

            return this;
        }

        public Product ProductUpdatedEvent()
        {
            AddDomainEvent(new ProductUpdatedDomainEvent(this));

            return this;
        }

        public Product ProductDeletedEvent()
        {
            AddDomainEvent(new ProductDeletedDomainEvent(this));

            return this;
        }
    }
}
