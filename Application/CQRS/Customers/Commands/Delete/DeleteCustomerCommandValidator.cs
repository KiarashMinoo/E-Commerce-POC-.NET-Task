using FluentValidation;

namespace Application.CQRS.Customers.Commands.Delete
{
    public class DeleteCustomerCommandValidator : AbstractValidator<DeleteCustomerCommand>
    {
        public DeleteCustomerCommandValidator()
        {
            RuleFor(a => a.Id).NotEmpty().Must(a => !a.Equals(Guid.Empty)).WithMessage("Invalid id pattern passed!");
        }
    }
}
