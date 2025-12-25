using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace EventBus.Messages.Common
{
    public class RabbitMQEventBus : IEventBus, IDisposable
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly string _exchangeName;
        private readonly string _queueName;

        public RabbitMQEventBus(string hostName, string exchangeName, string queueName)
        {
            _exchangeName = exchangeName;
            _queueName = queueName;

            var factory = new ConnectionFactory { HostName = hostName };
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();

            _channel.ExchangeDeclare(exchange: _exchangeName, type: ExchangeType.Fanout);
            _channel.QueueDeclare(queue: _queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
            _channel.QueueBind(queue: _queueName, exchange: _exchangeName, routingKey: string.Empty);
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

            Console.WriteLine($" [x] Sent {message}");
        }

        public void Subscribe<T, TH>() where T : class where TH : class
        {
            var eventName = typeof(T).Name;

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                var eventData = JsonSerializer.Deserialize<T>(message);

                Console.WriteLine($" [x] Received {message}");

                // Use reflection to create handler instance and invoke Handle method
                if (typeof(TH).GetMethod("Handle") != null)
                {
                    var handler = Activator.CreateInstance(typeof(TH));
                    typeof(TH).GetMethod("Handle")?.Invoke(handler, new object[] { eventData });
                }
            };

            _channel.BasicConsume(queue: _queueName, autoAck: true, consumer: consumer);
        }

        public void Dispose()
        {
            _channel?.Close();
            _connection?.Close();
        }
    }
}

