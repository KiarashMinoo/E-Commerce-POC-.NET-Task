namespace Api.Dto.Products
{
    public class ProductFetchDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;

        public int Quantity { get; set; }

        public decimal Price { get; set; }

        public string Image { get; set; } = null!;

        public string Thumbnail { get; set; } = null!;
    }
}
