using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Service2.RMQComponents.ComponentsFactories
{
 public   class PublisherFactory
    {
        //public static Publisher CreatePublisher(IConnection connection, IModel chanel, string queueName)
        //{ 

        //    return new Publisher( chanel,queueName );
        //}


        //public static Publisher CreatePublisher(IConnection connection, IModel chanel, string queueName,
        //EventHandler<BasicDeliverEventArgs> receivedHandler)
        //{

        //    var consumer = ConsumerFactory.CreateConsumer(chanel, queueName);


        //    return new Publisher(connection, chanel, queueName, consumer, receivedHandler
        //    );
        //}


        //public static Publisher CreatePublisher(string hostName, string queueName,
        //    EventHandler<BasicDeliverEventArgs> receivedHandler)
        //{
        //    var connection = ConnectionFactory.CreateConection(hostName);
        //    var chanel = ChanelFactory.CreateChanel(queueName, connection);
        //    var consumer = ConsumerFactory.CreateConsumer(chanel, queueName );
        //    return new Publisher(connection, chanel,queueName,consumer,receivedHandler
        //    );
        //}

        //public static Publisher CreatePublisher(string hostName, string queueName )
        //{
        //    var connection = ConnectionFactory.CreateConection(hostName);
        //    var chanel = ChanelFactory.CreateChanel(queueName, connection); 
        //    return new Publisher(connection, chanel, queueName);
        //}


        public static Publisher CreatePublisher(string hostName, string queueName)
        {
            var connection = ConnectionFactory.CreateConection(hostName);
            var requestChanel = ChanelFactory.CreateRequestChanel(queueName, connection);
            return new Publisher(  requestChanel, queueName);
        }

        

        public static Publisher CreatePublisher(string hostName, string queueName,
            EventHandler<BasicDeliverEventArgs> receivedHandler)
        {
            var connection = ConnectionFactory.CreateConection(hostName);
            var requestChanel = ChanelFactory.CreateRequestChanel(queueName, connection);
            var responseChanel = ChanelFactory.CreateResponseChanel(queueName, connection);
            var consumer = ConsumerFactory.CreateConsumer(responseChanel, queueName);
            return new Publisher(requestChanel,responseChanel, queueName, receivedHandler, consumer);
        }


        public static Publisher CreatePublisher(string hostName, string queueName, IDictionary<string, object> arguments)
        {
            var connection = ConnectionFactory.CreateConection(hostName);
            var requestChanel = ChanelFactory.CreateRequestChanel(queueName, connection, arguments);
            
            return new Publisher(requestChanel, queueName);
        }



        public static Publisher CreatePublisher(string hostName, string queueName,
            EventHandler<BasicDeliverEventArgs> receivedHandler, IDictionary<string, object> arguments)
        {
            var connection = ConnectionFactory.CreateConection(hostName);
            var requestChanel = ChanelFactory.CreateRequestChanel(queueName, connection, arguments);
            var responseChanel = ChanelFactory.CreateResponseChanel(queueName, connection, arguments);
            var consumer = ConsumerFactory.CreateConsumer(responseChanel, queueName);
            return new Publisher(requestChanel, responseChanel, queueName, receivedHandler, consumer);
        }



    }
}
