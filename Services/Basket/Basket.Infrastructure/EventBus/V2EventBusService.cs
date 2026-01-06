using EventBus.Messages.Common;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace Basket.Infrastructure.EventBus
{
    public class V2EventBusService : IV2EventBus, IDisposable
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly string _exchangeName;

        public V2EventBusService(string hostName, string exchangeName)
        {
            _exchangeName = exchangeName;

            var factory = new ConnectionFactory { HostName = hostName };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();

            _channel.ExchangeDeclare(exchange: _exchangeName, type: ExchangeType.Fanout);
        }

        public void Publish<T>(T eventMessage) where T : class
        {
            var message = JsonSerializer.Serialize(eventMessage);
            var body = Encoding.UTF8.GetBytes(message);

            _channel.BasicPublish(
                exchange: _exchangeName,
                routingKey: string.Empty,
                basicProperties: null,
                body: body);
        }

        public void Subscribe<T, TH>() where T : class where TH : class
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            _channel?.Close();
            _connection?.Close();
        }
    }
}
