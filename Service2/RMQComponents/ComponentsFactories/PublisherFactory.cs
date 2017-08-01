using System;
 
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Service2.RMQComponents.ComponentsFactories
{
 public   class PublisherFactory
    {
        public static Publisher CreatePublisher(IConnection connection, IModel chanel, string queueName)
        { 
            
            return new Publisher(connection,chanel,queueName );
        }


        public static Publisher CreatePublisher(IConnection connection, IModel chanel, string queueName,
        EventHandler<BasicDeliverEventArgs> receivedHandler)
        {
            
            var consumer = ConsumerFactory.CreateConsumer(chanel, queueName);
             

            return new Publisher(connection, chanel, queueName, consumer, receivedHandler
            );
        }


        public static Publisher CreatePublisher(string hostName, string queueName,
            EventHandler<BasicDeliverEventArgs> receivedHandler)
        {
            var connection = ConnectionFactory.CreateConection(hostName);
            var chanel = ChanelFactory.CreateChanel(queueName, connection);
            var consumer = ConsumerFactory.CreateConsumer(chanel, queueName );
            return new Publisher(connection, chanel,queueName,consumer,receivedHandler
            );
        }

        public static Publisher CreatePublisher(string hostName, string queueName )
        {
            var connection = ConnectionFactory.CreateConection(hostName);
            var chanel = ChanelFactory.CreateChanel(queueName, connection); 
            return new Publisher(connection, chanel, queueName);
        }


    }
}
