using FluentValidation;

namespace Application.CQRS.Customers.Commands.Update
{
    public class UpdateCustomerCommandValidator : AbstractValidator<UpdateCustomerCommand>
    {
        public UpdateCustomerCommandValidator()
        {
            RuleFor(a => a.Id).NotEmpty().Must(a => !a.Equals(Guid.Empty)).WithMessage("Invalid id pattern passed!");
            RuleFor(a => a.FullName).NotNull().NotEmpty();
            RuleFor(a => a.EMail).NotNull().NotEmpty().EmailAddress();
            RuleFor(a => a.Cell).Matches(@"^(\+\d{1,2}\s?)?1?\-?\.?\s?\(?\d{3}\)?[\s.-]?\d{3}[\s.-]?\d{4}$").WithMessage("Cell number is not valid");
        }
    }
}
