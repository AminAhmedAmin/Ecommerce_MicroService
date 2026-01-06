using EventBus.Messages.Events;
using MediatR;
using Ordering.Application.Commands;

namespace Ordering.API.EventBus
{
    public class BasketCheckoutConsumerV2
    {
        private readonly IMediator _mediator;
        private readonly ILogger<BasketCheckoutConsumerV2> _logger;

        public BasketCheckoutConsumerV2(IMediator mediator, ILogger<BasketCheckoutConsumerV2> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        public async Task Handle(BasketCheckoutEventV2 checkoutEvent)
        {
            _logger.LogInformation($"[V2] Received basket checkout event for user: {checkoutEvent.UserName}. Version: {checkoutEvent.Version}");
            _logger.LogInformation($"[V2] Total Price with Tax: {checkoutEvent.TotalPriceWithTax}");

            try
            {
                var command = new CheckoutOrderCommand
                {
                    UserName = checkoutEvent.UserName,
                    TotalPrice = checkoutEvent.TotalPrice, // Use original price for order, or V2 logic. 
                    // Assuming Order entity doesn't change, we map what we have.
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
                _logger.LogInformation($"[V2] Order created successfully for user: {checkoutEvent.UserName}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[V2] Error processing basket checkout event for user: {checkoutEvent.UserName}");
                throw;
            }
        }
    }
}
