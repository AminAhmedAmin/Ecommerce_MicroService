using FluentValidation;
using Ordering.Application.Commands;

namespace Ordering.Application.Validators
{
    public class DeleteOrderCommandValidator : AbstractValidator<DeleteOrderCommand>
    {
        public DeleteOrderCommandValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("Id must be greater than 0");
        }
    }
}

