using FluentValidation;

namespace Application.CQRS.Products.Commands.Create
{
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(a => a.Name).NotNull().NotEmpty();
            RuleFor(a => a.Price).NotNull().NotEmpty().GreaterThan(1);
        }
    }
}
