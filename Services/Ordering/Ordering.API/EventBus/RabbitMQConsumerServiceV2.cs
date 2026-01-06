using EventBus.Messages.Events;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace Ordering.API.EventBus
{
    public class RabbitMQConsumerServiceV2 : BackgroundService
    {
        private readonly IConnectionFactory _connectionFactory;
        private IConnection _connection;
        private IModel _channel;
        private readonly string _exchangeName;
        private readonly string _queueName;
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<RabbitMQConsumerServiceV2> _logger;

        public RabbitMQConsumerServiceV2(
            string hostName,
            string exchangeName,
            string queueName,
            IServiceProvider serviceProvider,
            ILogger<RabbitMQConsumerServiceV2> logger)
        {
            _exchangeName = exchangeName;
            _queueName = queueName;
            _serviceProvider = serviceProvider;
            _logger = logger;
            _connectionFactory = new ConnectionFactory { HostName = hostName };
        }

        private void InitializeRabbitMQListener()
        {
            _connection = _connectionFactory.CreateConnection();
            _channel = _connection.CreateModel();

            _channel.ExchangeDeclare(exchange: _exchangeName, type: ExchangeType.Fanout);
            _channel.QueueDeclare(queue: _queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
            _channel.QueueBind(queue: _queueName, exchange: _exchangeName, routingKey: string.Empty);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    InitializeRabbitMQListener();

                    var consumer = new EventingBasicConsumer(_channel);
                    consumer.Received += async (model, ea) =>
                    {
                        if (stoppingToken.IsCancellationRequested)
                            return;

                        var body = ea.Body.ToArray();
                        var message = Encoding.UTF8.GetString(body);

                        _logger.LogInformation($"[V2] Received message: {message}");

                        try
                        {
                            var checkoutEvent = JsonSerializer.Deserialize<BasketCheckoutEventV2>(message);
                            if (checkoutEvent != null)
                            {
                                using (var scope = _serviceProvider.CreateScope())
                                {
                                    var consumerService = scope.ServiceProvider.GetRequiredService<BasketCheckoutConsumerV2>();
                                    await consumerService.Handle(checkoutEvent);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError(ex, $"[V2] Error processing message: {message}");
                        }
                    };

                    _channel.BasicConsume(queue: _queueName, autoAck: true, consumer: consumer);
                    
                    _logger.LogInformation("[V2] RabbitMQ Consumer Service started successfully.");

                    // Keep the task alive while connected
                    while (!stoppingToken.IsCancellationRequested)
                    {
                        await Task.Delay(1000, stoppingToken);
                    }
                }
                catch (Exception ex)
                {
                     _logger.LogError(ex, "[V2] Failed to connect to RabbitMQ, retrying in 5s...");
                     await Task.Delay(5000, stoppingToken);
                }
            }
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("[V2] RabbitMQ Consumer Service is stopping.");
            return base.StopAsync(cancellationToken);
        }

        public override void Dispose()
        {
            _channel?.Close();
            _connection?.Close();
            base.Dispose();
        }
    }
}
