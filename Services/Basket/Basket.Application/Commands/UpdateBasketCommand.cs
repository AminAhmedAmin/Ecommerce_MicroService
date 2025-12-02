using Basket.Application.Responses;
using Basket.Core.Entities;
using MediatR;

namespace Basket.Application.Commands
{
    public class UpdateBasketCommand : IRequest<ShoppingCartResponse>
    {
        public ShoppingCart Cart { get; set; }

        public UpdateBasketCommand(ShoppingCart cart)
        {
            Cart = cart;
        }
    }
}
