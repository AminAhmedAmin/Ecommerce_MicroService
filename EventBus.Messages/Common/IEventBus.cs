namespace EventBus.Messages.Common
{
    public interface IEventBus
    {
        void Publish<T>(T eventMessage) where T : class;
        void Subscribe<T, TH>() where T : class where TH : class;
    }
}

