using Basket.Core.Entities;
using MediatR;

namespace Basket.Application.Commands
{
    public class CheckoutBasketCommand : IRequest<bool>
    {
        public BasketCheckout BasketCheckout { get; set; }

        public CheckoutBasketCommand(BasketCheckout basketCheckout)
        {
            BasketCheckout = basketCheckout;
        }
    }
}

