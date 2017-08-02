using System.CodeDom;
using System.Collections.Generic;
using RabbitMQ.Client;

namespace Service2.RMQComponents.ComponentsFactories
{
   public class ChanelFactory
    {

        private static string GetRequestQueueName(string queueName)
        {
            return ExtensionMethods.CreateRoutinKey(queueName, EventType.Request);  
        }

        private static string GetResponseQeueName (string queueName)
        {
              return ExtensionMethods.CreateRoutinKey(queueName, EventType.Response); 
        }

     

        /// <summary>
        /// Create the queue chanel to RabbitMq. 
        /// </summary>
        /// <param name="queueName"></param>
        /// <param name="connection"></param>
        /// <returns></returns>
        public static IModel CreateRequestChanel(string queueName, IConnection connection)
        {
            // Create and return a fresh channel, session, and model.
            var chanel = connection.CreateModel();
            var requestQueueName  = GetRequestQueueName(queueName);
            chanel.QueueDeclare(queue: requestQueueName,
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null);
            return chanel;
        }

        /// <summary>
        /// Create the queue chanel to RabbitMq. 
        /// </summary>
        /// <param name="queueName"></param>
        /// <param name="connection"></param>
        /// <returns></returns>
        public static IModel CreateRequestChanel(string queueName, IConnection connection,
            IDictionary<string, object> arguments)
        {
            // Create and return a fresh channel, session, and model.
            var chanel = connection.CreateModel();

            var requestQueueName = GetRequestQueueName(queueName);
            chanel.QueueDeclare(queue: requestQueueName,
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: arguments);
            chanel.a
            return chanel;
        }




        /// <summary>
        /// Create the queue chanel to RabbitMq. 
        /// </summary>
        /// <param name="queueName"></param>
        /// <param name="connection"></param>
        /// <returns></returns>
        public static IModel CreateResponseChanel(string queueName, IConnection connection)
        {
            // Create and return a fresh channel, session, and model.
            var chanel = connection.CreateModel();

            var responseQueueName = GetResponseQeueName(queueName);
            chanel.QueueDeclare(queue: responseQueueName,
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null);
            return chanel;
        }

        /// <summary>
        /// Create the queue chanel to RabbitMq. 
        /// </summary>
        /// <param name="queueName"></param>
        /// <param name="connection"></param>
        /// <returns></returns>
        public static IModel CreateResponseChanel(string queueName, IConnection connection,
            IDictionary<string, object> arguments)
        {
            var chanel = connection.CreateModel();

            var requestQueueName = GetRequestQueueName(queueName);
            chanel.QueueDeclare(queue: requestQueueName,
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: arguments);
            return chanel;
        }
    }
}
