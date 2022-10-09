using Domain.Customers;
using MongoDB.Bson.Serialization;

namespace Infrastructure.Domains.Customers
{
    internal class CustomerMongoDbClassMap
    {
        public static void Register()
        {
            if (!BsonClassMap.IsClassMapRegistered(typeof(Customer)))
                BsonClassMap.RegisterClassMap<Customer>(cm =>
                {
                    cm.AutoMap();
                    cm.MapIdProperty(a => a.Id);
                    cm.MapProperty(a => a.FullName);
                    cm.MapProperty(a => a.EMail);
                    cm.MapProperty(a => a.Cell);
                });
        }
    }
}
