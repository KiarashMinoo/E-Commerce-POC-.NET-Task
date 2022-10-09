using BuildingBlocks.Domain.BusinessRules;

namespace Domain.Products.BusinessRules.ProductNameMustBeUnique
{
    internal class ProductNameMustBeUniqueBusinessRule : IBusinessRule
    {
        private readonly IProductNameMustBeUniqueHandler handler;
        private readonly Guid productId;
        private readonly string name;

        public string Message => $"The product name has already exists in database.";

        public ProductNameMustBeUniqueBusinessRule(IProductNameMustBeUniqueHandler handler, Guid productId, string name)
        {
            this.handler = handler;
            this.productId = productId;
            this.name = name;
        }

        public bool IsBroken() => handler.ProductNameExistsByName(productId, name);
    }
}
