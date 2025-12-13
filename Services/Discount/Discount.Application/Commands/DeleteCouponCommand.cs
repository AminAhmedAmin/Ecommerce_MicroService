using MediatR;

namespace Discount.Application.Commands
{
    public class DeleteCouponCommand : IRequest<bool>
    {
        public int Id { get; set; }

        public DeleteCouponCommand(int id)
        {
            Id = id;
        }
    }
}

