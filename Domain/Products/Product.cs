using BuildingBlocks.Domain;
using Domain.Products.BusinessRules;

namespace Domain.Products
{
    public class Product : EntityBase, IAggregateRoot
    {
        public string Name { get; } = null!;

        public int Quantity { get; private set; }

        public decimal Price { get; }

        //Used For Ef
        private Product()
        {
        }

        public Product(string name, decimal price)
        {
            Name = name;
            Price = price;
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
    }
}
