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
                    cm.MapIdField(a => a.Id);
                    cm.MapCreator(a => new Customer(a.Id, a.FullName, a.EMail, a.Cell));
                });
        }
    }
}
