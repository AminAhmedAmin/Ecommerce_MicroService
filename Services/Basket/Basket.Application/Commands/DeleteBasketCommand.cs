using MediatR;

namespace Basket.Application.Commands
{
    public class DeleteBasketCommand : IRequest<bool>
    {
        public string UserName { get; set; }

        public DeleteBasketCommand(string userName)
        {
            UserName = userName;
        }
    }
}
