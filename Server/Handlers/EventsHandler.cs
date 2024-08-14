using System;
using System.Text;
using RabbitMQ.Client;
using XCore.Server.Utilities;

namespace XCore.Server.Handlers
{
    public class XCoreEvents
    {
        private readonly ConsoleUtility _logger = new ConsoleUtility();
        private IModel _channel;
        private IConnection _connection;

        public void XCoreEventHandlerInitializer(IniFile conf, string resourceName)
        {
            var factory = new ConnectionFactory()
            {
                HostName = conf.GetValue("RabbitMQ", "Hostname"),
                UserName = conf.GetValue("RabbitMQ", "Username"),
                Password = conf.GetValue("RabbitMQ", "Password")
            };

            _logger.Log("Attempting", $"{resourceName} Creating connection to RabbitMQ", ConsoleColor.DarkYellow, ConsoleColor.Yellow);

            _connection = factory.CreateConnection();
            _logger.Log("Success", $"{resourceName} Successfully connected to RabbitMQ", ConsoleColor.DarkGreen, ConsoleColor.Green);

            _channel = _connection.CreateModel();
        }

        public void RegisterXCoreEvent(string eventName)
        {
            if (_channel == null)
            {
                throw new InvalidOperationException("RabbitMQ channel is not initialized.");
            }

            _logger.Log("Success", $"Event {eventName} registered successfully.", ConsoleColor.DarkGreen, ConsoleColor.Green);
            _channel.QueueDeclare(queue: eventName,
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null);
        }

        public void FireXCoreEvent(string eventName, string message)
        {
            if (_channel == null)
            {
                throw new InvalidOperationException("RabbitMQ channel is not initialized.");
            }

            var body = Encoding.UTF8.GetBytes(message);

            _channel.BasicPublish(exchange: "",
                routingKey: eventName,
                basicProperties: null,
                body: body);
            CitizenFX.Core.Debug.WriteLine($" [x] Sent {message}");
            _logger.Log("Success", $"Event {eventName} has been fired.", ConsoleColor.DarkGreen, ConsoleColor.Green);
        }

        public void Close()
        {
            _channel?.Close();
            _connection?.Close();
        }
    }
}
