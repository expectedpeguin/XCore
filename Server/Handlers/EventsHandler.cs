using System;
using System.Text;
using CitizenFX.Core;
using RabbitMQ.Client;
using XCore.Server.Utilities;

namespace XCore.Server
{
    public class XCoreEvents
    {
        private readonly ConsoleUtility _logger = new ConsoleUtility();
        private IModel _channel;
        public void XCoreEventHandlerInitializer(IniFile conf, string resourceName)
        {
            var factory = new ConnectionFactory()
            {
                HostName = conf.GetValue("RabbitMQ", "Hostname") ,
                UserName = conf.GetValue("RabbitMQ", "Username"),
                Password = conf.GetValue("RabbitMQ", "Password")
            };

            _logger.Log("Attempting",$"{resourceName} Creating connection to RabbitMQ", ConsoleColor.DarkYellow,ConsoleColor.Yellow);
            using (var connection = factory.CreateConnection())
            {
                _logger.Log("Success",$"{resourceName} Successfully connected to RabbitMQ", ConsoleColor.DarkGreen,ConsoleColor.Green);
                using (var channel = connection.CreateModel())
                {
                    _channel = channel;
                }
            }
        }

        public void RegisterXCoreEvent(string eventName)
        {
            _logger.Log("Success",$"Event {eventName} registered succesfully.", ConsoleColor.DarkGreen,ConsoleColor.Green);
            _channel.QueueDeclare(queue: eventName,
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null);
                        
            
        }

        public void FireXCoreEvent(string eventName, string message)
        {
            var body = Encoding.UTF8.GetBytes(message);

            _channel.BasicPublish(exchange: "",
                routingKey: eventName,
                basicProperties: null,
                body: body);
            CitizenFX.Core.Debug.WriteLine($" [x] Sent {message}");
            _logger.Log("Success",$"Event {eventName} has been fired.", ConsoleColor.DarkGreen,ConsoleColor.Green);
        }
    }
}