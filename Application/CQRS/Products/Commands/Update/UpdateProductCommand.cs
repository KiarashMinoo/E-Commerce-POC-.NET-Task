using BuildingBlocks.Application.CQRS.Commands;

namespace Application.CQRS.Products.Commands.Update
{
    public class UpdateProductCommand : ICommand<UpdateProductCommandResultDto>
    {
        public Guid Id { get; }

        public string Name { get; }

        public int Quantity { get; }

        public decimal Price { get; }

        public Stream? Image { get; }

        public UpdateProductCommand(Guid id, string name, int quantity, decimal price, Stream? image)
        {
            Id = id;
            Name = name;
            Quantity = quantity;
            Price = price;
            Image = image;
        }
    }
}
