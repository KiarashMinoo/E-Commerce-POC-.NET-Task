using BuildingBlocks.Application.CQRS.Commands;

namespace Application.CQRS.Products.Commands.Create
{
    public class CreateProductCommand : ICommand<CreateProductCommandResultDto>
    {
        public string Name { get; } 

        public int Quantity { get; }

        public decimal Price { get; }        

        public CreateProductCommand(string name, int quantity, decimal price)
        {
            Name = name;
            Quantity = quantity;
            Price = price;
        }
    }
}
