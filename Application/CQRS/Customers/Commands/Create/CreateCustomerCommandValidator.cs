using FluentValidation;

namespace Application.CQRS.Customers.Commands.Create
{
    public class CreateCustomerCommandValidator : AbstractValidator<CreateCustomerCommand>
    {
        public CreateCustomerCommandValidator()
        {
            RuleFor(a => a.FullName).NotNull().NotEmpty();
            RuleFor(a => a.EMail).NotNull().NotEmpty().EmailAddress();
            RuleFor(a => a.Cell).Matches(@"^(\+\d{1,2}\s?)?1?\-?\.?\s?\(?\d{3}\)?[\s.-]?\d{3}[\s.-]?\d{4}$").WithMessage("Cell number is not valid");
        }
    }
}
