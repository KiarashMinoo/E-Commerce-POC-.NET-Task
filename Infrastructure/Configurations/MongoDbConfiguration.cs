namespace Infrastructure.Configurations
{
    internal class MongoDbConfiguration
    {
        public string ConnectionString { get; set; } = null!;
        public string DatabaseName { get; set; } = null!;

        public string CustomersCollectionName { get; set; } = null!;
        public string ProductsCollectionName { get; set; } = null!;
    }
}
