using System.Collections.Generic;
using RabbitMQ.Client;

namespace Service2.RMQComponents.ComponentsFactories
{
    class ChanelFactory
    {
        /// <summary>
        /// Create the queue chanel to RabbitMq. 
        /// </summary>
        /// <param name="queueName"></param>
        /// <param name="connection"></param>
        /// <returns></returns>
        public static IModel CreateChanel(string queueName, IConnection connection)
        {
            // Create and return a fresh channel, session, and model.
            var channel = connection.CreateModel();

            channel.QueueDeclare(queue: queueName,
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null);
            return channel;
        }

        /// <summary>
        /// Create the queue chanel to RabbitMq. 
        /// </summary>
        /// <param name="queueName"></param>
        /// <param name="connection"></param>
        /// <returns></returns>
        public static IModel CreateChanel(string queueName, IConnection connection,
            IDictionary<string, object> arguments)
        {
            // Create and return a fresh channel, session, and model.
            var channel = connection.CreateModel();

            channel.QueueDeclare(queue: queueName,
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: arguments);
            return channel;
        }
    }
}
