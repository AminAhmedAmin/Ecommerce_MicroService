using FluentValidation;
using Ordering.Application.Queries;

namespace Ordering.Application.Validators
{
    public class GetOrderListQueryValidator : AbstractValidator<GetOrderListQuery>
    {
        public GetOrderListQueryValidator()
        {
            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("UserName is required")
                .MaximumLength(50).WithMessage("UserName must not exceed 50 characters");
        }
    }
}

