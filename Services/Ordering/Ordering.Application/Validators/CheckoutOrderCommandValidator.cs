using FluentValidation;
using Ordering.Application.Commands;

namespace Ordering.Application.Validators
{
    public class CheckoutOrderCommandValidator : AbstractValidator<CheckoutOrderCommand>
    {
        public CheckoutOrderCommandValidator()
        {
            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("UserName is required")
                .MaximumLength(50).WithMessage("UserName must not exceed 50 characters");

            RuleFor(x => x.TotalPrice)
                .GreaterThan(0).WithMessage("TotalPrice must be greater than 0");

            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("FirstName is required")
                .MaximumLength(50).WithMessage("FirstName must not exceed 50 characters");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("LastName is required")
                .MaximumLength(50).WithMessage("LastName must not exceed 50 characters");

            RuleFor(x => x.EmailAddress)
                .NotEmpty().WithMessage("EmailAddress is required")
                .EmailAddress().WithMessage("EmailAddress must be a valid email address")
                .MaximumLength(100).WithMessage("EmailAddress must not exceed 100 characters");

            RuleFor(x => x.AddressLine)
                .NotEmpty().WithMessage("AddressLine is required")
                .MaximumLength(200).WithMessage("AddressLine must not exceed 200 characters");

            RuleFor(x => x.Country)
                .NotEmpty().WithMessage("Country is required")
                .MaximumLength(50).WithMessage("Country must not exceed 50 characters");

            RuleFor(x => x.State)
                .NotEmpty().WithMessage("State is required")
                .MaximumLength(50).WithMessage("State must not exceed 50 characters");

            RuleFor(x => x.ZipCode)
                .NotEmpty().WithMessage("ZipCode is required")
                .MaximumLength(20).WithMessage("ZipCode must not exceed 20 characters");

            RuleFor(x => x.CardName)
                .NotEmpty().WithMessage("CardName is required")
                .MaximumLength(100).WithMessage("CardName must not exceed 100 characters");

            RuleFor(x => x.CardNumber)
                .NotEmpty().WithMessage("CardNumber is required")
                .Matches(@"^\d{13,19}$").WithMessage("CardNumber must be between 13 and 19 digits");

            RuleFor(x => x.Expiration)
                .NotEmpty().WithMessage("Expiration is required")
                .Matches(@"^(0[1-9]|1[0-2])\/\d{2}$").WithMessage("Expiration must be in MM/YY format");

            RuleFor(x => x.CVV)
                .NotEmpty().WithMessage("CVV is required")
                .Matches(@"^\d{3,4}$").WithMessage("CVV must be 3 or 4 digits");

            RuleFor(x => x.PaymentMethod)
                .GreaterThanOrEqualTo(0).WithMessage("PaymentMethod must be a valid value");
        }
    }
}

