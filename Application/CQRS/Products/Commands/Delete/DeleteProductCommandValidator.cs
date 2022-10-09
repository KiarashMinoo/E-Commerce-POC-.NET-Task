using FluentValidation;

namespace Application.CQRS.Products.Commands.Delete
{
    public class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
    {
        public DeleteProductCommandValidator()
        {
            RuleFor(a => a.Id).NotEmpty().Must(a => !a.Equals(Guid.Empty)).WithMessage("Invalid id pattern passed!");
        }
    }
}
