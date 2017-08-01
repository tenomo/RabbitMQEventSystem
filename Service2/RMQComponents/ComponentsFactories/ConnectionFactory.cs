using RabbitMQ.Client;

namespace Service2.RMQComponents.ComponentsFactories
{
   public class ConnectionFactory
    {
        /// <summary>
        /// Create conection to RabbitMq.  
        /// </summary>
        /// <param name="hostName">RabbitMq host name</param>
        /// <param name="queueName">Queue name </param>
        /// <param name="socketReadTimeout">Timeout setting for socket read operations (in milliseconds).</param>
        /// <param name="socketWriteTimeout">Timeout setting for socket write operations (in milliseconds).</param>
        /// <returns></returns>
        public static IConnection CreateConection(string hostName, int socketReadTimeout = 30000,
            int socketWriteTimeout = 30000)
        {
            var factory = new RabbitMQ.Client.ConnectionFactory()
            {
                HostName = hostName,
                SocketReadTimeout = socketReadTimeout,
                SocketWriteTimeout = socketWriteTimeout
            };
            var connection = factory.CreateConnection();

            return connection;
        }
    }
}
