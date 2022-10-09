using BuildingBlocks.Application.CQRS.Commands;

namespace Application.CQRS.Products.Commands.Create
{
    public class CreateProductCommand : ICommand<CreateProductCommandResultDto>
    {
        public string Name { get; }

        public int Quantity { get; }

        public decimal Price { get; }

        public Stream Image { get; }

        public CreateProductCommand(string name, int quantity, decimal price, Stream image)
        {
            Name = name;
            Quantity = quantity;
            Price = price;
            Image = image;
        }
    }
}
