using AutoMapper;
using Basket.Application.Commands;
using Basket.Core.Entities;
using Basket.Core.Repositories;
using EventBus.Messages.Common;
using EventBus.Messages.Events;
using MediatR;

namespace Basket.Application.Handlers
{
    public class CheckoutBasketCommandHandler : IRequestHandler<CheckoutBasketCommand, bool>
    {
        private readonly IBasketRepository _repository;
        private readonly IEventBus _eventBus;
        private readonly IMapper _mapper;

        public CheckoutBasketCommandHandler(IBasketRepository repository, IEventBus eventBus, IMapper mapper)
        {
            _repository = repository;
            _eventBus = eventBus;
            _mapper = mapper;
        }

        public async Task<bool> Handle(CheckoutBasketCommand request, CancellationToken cancellationToken)
        {
            // Get the basket
            var basket = await _repository.GetCartAsync(request.BasketCheckout.UserName);
            if (basket == null)
            {
                return false;
            }

            // Map to event
            var checkoutEvent = new BasketCheckoutEvent
            {
                UserName = request.BasketCheckout.UserName,
                TotalPrice = request.BasketCheckout.TotalPrice,
                FirstName = request.BasketCheckout.FristName, // Note: typo in original entity
                LastName = request.BasketCheckout.LastName,
                EmailAddress = request.BasketCheckout.EmailAddress,
                AddressLine = request.BasketCheckout.AddressLine,
                Country = request.BasketCheckout.Country,
                State = request.BasketCheckout.City, // Mapping City to State
                ZipCode = request.BasketCheckout.ZipCode,
                CardName = request.BasketCheckout.CardName,
                CardNumber = request.BasketCheckout.CardNumber,
                Expiration = request.BasketCheckout.Expiration,
                CVV = request.BasketCheckout.CVV,
                PaymentMethod = request.BasketCheckout.PaymentMethod
            };

            // Publish event
            _eventBus.Publish(checkoutEvent);

            // Delete basket after checkout
            await _repository.DeleteCartAsync(request.BasketCheckout.UserName);

            return true;
        }
    }
}

