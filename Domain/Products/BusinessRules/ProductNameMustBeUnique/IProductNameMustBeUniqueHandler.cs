namespace Domain.Products.BusinessRules.ProductNameMustBeUnique
{
    public interface IProductNameMustBeUniqueHandler
    {
        bool ProductNameExistsByName(Guid productId, string name);
    }
}
