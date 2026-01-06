using EventBus.Messages.Common;
using Basket.Application.Commands;
using Basket.Core.Entities;
using MediatR;

namespace Basket.Application.Commands
{
    public class CheckoutBasketCommandV2 : IRequest<bool>
    {
        public BasketCheckout BasketCheckout { get; set; }

        public CheckoutBasketCommandV2(BasketCheckout basketCheckout)
        {
            BasketCheckout = basketCheckout;
        }
    }
}
