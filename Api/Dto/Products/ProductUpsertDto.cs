namespace Api.Dto.Products
{
    public class ProductUpsertDto
    {
        public string Name { get; set; } = null!;

        public int Quantity { get; set; }

        public decimal Price { get; set; }

        public IFormFile? Image { get; set; }
    }
}
