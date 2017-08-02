using System;
using Newtonsoft.Json.Serialization;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Service2.RMQComponents.ComponentsFactories
{
  public  class ListenerFactory
    {

        public static Listener CreateListener(string hostName,  string queueName, EventHandler<BasicDeliverEventArgs> receivedHandler)
        {
            var connection = ConnectionFactory.CreateConection( hostName);
            var chanel = ChanelFactory.CreateRequestChanel(queueName,connection);
            var consumer = ConsumerFactory.CreateConsumer(chanel, queueName);
            return new Listener(chanel, consumer, queueName, receivedHandler);
        }



        public static Listener CreateListener(string hostName, string queueName, EventHandler<BasicDeliverEventArgs> receivedHandler, ResponseBynarySender binarySender)
        {
            var connection = ConnectionFactory.CreateConection(hostName);
            var requestChanel = ChanelFactory.CreateRequestChanel(queueName, connection);
    

            var sender = PublisherFactory.CreateResponseSender(hostName, queueName);

            var consumer = ConsumerFactory.CreateConsumer(requestChanel, queueName);
            return new Listener(requestChanel, consumer, queueName, receivedHandler,sender, binarySender);
        }

        public static Listener CreateListener(string hostName, string queueName, EventHandler<BasicDeliverEventArgs> receivedHandler, ResponseObjectSender objectSender)
        {
            var connection = ConnectionFactory.CreateConection(hostName);
            var requestChanel = ChanelFactory.CreateRequestChanel(queueName, connection);


            var sender = PublisherFactory.CreateResponseSender(hostName, queueName);

            var consumer = ConsumerFactory.CreateConsumer(requestChanel, queueName);
            return new Listener(requestChanel, consumer, queueName, receivedHandler, sender, objectSender);
        }

 
    }
}
