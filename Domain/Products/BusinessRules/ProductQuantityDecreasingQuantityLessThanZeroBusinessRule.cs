using BuildingBlocks.Domain.BusinessRules;

namespace Domain.Products.BusinessRules
{
    internal class ProductQuantityDecreasingQuantityLessThanZeroBusinessRule : IBusinessRule
    {
        private readonly Product product;
        private readonly int valueToSubract;

        public ProductQuantityDecreasingQuantityLessThanZeroBusinessRule(Product product, int valueToSubract)
        {
            this.product = product;
            this.valueToSubract = valueToSubract;
        }

        public string Message => $"The available quantity of product {product.Name} is {product.Quantity}, the amount of {valueToSubract} is more than avaiable quantity.";

        public bool IsBroken()
        {
            var value = product.Quantity - valueToSubract;
            return value < 0;
        }
    }
}
