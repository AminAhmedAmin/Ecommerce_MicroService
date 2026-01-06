using AutoMapper;
using Basket.Application.Commands;
using Basket.Core.Repositories;
using EventBus.Messages.Common;
using EventBus.Messages.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Basket.Application.Handlers
{
    public class CheckoutBasketCommandHandlerV2 : IRequestHandler<CheckoutBasketCommandV2, bool>
    {
        private readonly IBasketRepository _repository;
        private readonly IV2EventBus _eventBus;
        private readonly IMapper _mapper;
        private readonly ILogger<CheckoutBasketCommandHandlerV2> _logger;

        public CheckoutBasketCommandHandlerV2(IBasketRepository repository, IV2EventBus eventBus, IMapper mapper, ILogger<CheckoutBasketCommandHandlerV2> logger)
        {
            _repository = repository;
            _eventBus = eventBus;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<bool> Handle(CheckoutBasketCommandV2 request, CancellationToken cancellationToken)
        {
            var basket = await _repository.GetCartAsync(request.BasketCheckout.UserName);
            if (basket == null)
            {
                return false;
            }

            var checkoutEvent = new BasketCheckoutEventV2
            {
                UserName = request.BasketCheckout.UserName,
                TotalPrice = request.BasketCheckout.TotalPrice,
                TotalPriceWithTax = request.BasketCheckout.TotalPrice * 1.1m, // Example tax calculation for V2
                FirstName = request.BasketCheckout.FristName,
                LastName = request.BasketCheckout.LastName,
                EmailAddress = request.BasketCheckout.EmailAddress,
                AddressLine = request.BasketCheckout.AddressLine,
                Country = request.BasketCheckout.Country,
                State = request.BasketCheckout.City,
                ZipCode = request.BasketCheckout.ZipCode,
                CardName = request.BasketCheckout.CardName,
                CardNumber = request.BasketCheckout.CardNumber,
                Expiration = request.BasketCheckout.Expiration,
                CVV = request.BasketCheckout.CVV,
                PaymentMethod = request.BasketCheckout.PaymentMethod,
                Version = 2
            };

            _eventBus.Publish(checkoutEvent);
            _logger.LogInformation("BasketCheckoutEventV2 published for {UserName}", request.BasketCheckout.UserName);

            await _repository.DeleteCartAsync(request.BasketCheckout.UserName);

            return true;
        }
    }
}
