using FluentValidation;

namespace Application.CQRS.Products.Commands.Update
{
    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidator()
        {
            RuleFor(a => a.Id).NotEmpty().Must(a => !a.Equals(Guid.Empty)).WithMessage("Invalid id pattern passed!");
            RuleFor(a => a.Name).NotNull().NotEmpty();
            RuleFor(a => a.Price).NotNull().NotEmpty().GreaterThan(1);
        }
    }
}
