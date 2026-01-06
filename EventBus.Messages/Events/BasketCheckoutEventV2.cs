using EventBus.Messages.Events;

namespace EventBus.Messages.Events
{
    public class BasketCheckoutEventV2 : BasketCheckoutEvent
    {
        public decimal TotalPriceWithTax { get; set; }
        public int Version { get; set; } = 2;
    }
}
