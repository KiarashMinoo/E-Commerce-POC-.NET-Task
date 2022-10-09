namespace Application.CQRS.Products.Queries.ListAll
{
    public class ListAllProductQueryResultDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;

        public int Quantity { get; set; } 

        public decimal Price { get; set; }
    }
}
