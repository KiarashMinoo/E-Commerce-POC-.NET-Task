using Domain.Products;
using MongoDB.Bson.Serialization;

namespace Infrastructure.Domains.Products
{
    internal class ProductMongoDbClassMap
    {
        public static void Register()
        {
            if (!BsonClassMap.IsClassMapRegistered(typeof(Product)))
                BsonClassMap.RegisterClassMap<Product>(cm =>
                {
                    cm.AutoMap();
                    cm.MapIdProperty(a => a.Id);
                    cm.MapProperty(a => a.Name);
                    cm.MapProperty(a => a.Quantity);
                    cm.MapProperty(a => a.Price);
                });
        }
    }
}
