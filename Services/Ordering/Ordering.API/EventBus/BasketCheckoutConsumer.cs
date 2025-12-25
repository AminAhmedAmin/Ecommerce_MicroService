using EventBus.Messages.Events;
using MediatR;
using Ordering.Application.Commands;
using System.Text.Json;

namespace Ordering.API.EventBus
{
    public class BasketCheckoutConsumer
    {
        private readonly IMediator _mediator;
        private readonly ILogger<BasketCheckoutConsumer> _logger;

        public BasketCheckoutConsumer(IMediator mediator, ILogger<BasketCheckoutConsumer> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        public async Task Handle(BasketCheckoutEvent checkoutEvent)
        {
            _logger.LogInformation($"Received basket checkout event for user: {checkoutEvent.UserName}");

            try
            {
                var command = new CheckoutOrderCommand
                {
                    UserName = checkoutEvent.UserName,
                    TotalPrice = checkoutEvent.TotalPrice,
                    FirstName = checkoutEvent.FirstName,
                    LastName = checkoutEvent.LastName,
                    EmailAddress = checkoutEvent.EmailAddress,
                    AddressLine = checkoutEvent.AddressLine,
                    Country = checkoutEvent.Country,
                    State = checkoutEvent.State,
                    ZipCode = checkoutEvent.ZipCode,
                    CardName = checkoutEvent.CardName,
                    CardNumber = checkoutEvent.CardNumber,
                    Expiration = checkoutEvent.Expiration,
                    CVV = checkoutEvent.CVV,
                    PaymentMethod = checkoutEvent.PaymentMethod
                };

                var result = await _mediator.Send(command);
                _logger.LogInformation($"Order created successfully for user: {checkoutEvent.UserName}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error processing basket checkout event for user: {checkoutEvent.UserName}");
                throw;
            }
        }
    }
}

