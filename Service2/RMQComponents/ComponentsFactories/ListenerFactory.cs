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



        public static Listener CreateListener(string hostName, string queueName, EventHandler<BasicDeliverEventArgs> receivedHandler)
        {
            var connection = ConnectionFactory.CreateConection(hostName);
            var requestChanel = ChanelFactory.CreateRequestChanel(queueName, connection);
            var responseChanel = ChanelFactory.CreateRequestChanel(queueName, connection);

            var sender = PublisherFactory.CreatePublisher(hostName, queueName);

            var consumer = ConsumerFactory.CreateConsumer(requestChanel, queueName);
            return new Listener(requestChanel, consumer, queueName, receivedHandler);
        }


        //public static Listener CreateListener(string hostName, string queueName,
        //  EventHandler<BasicDeliverEventArgs> receivedHandler,
        //  ResponseSender responseSender)
        //{
        //    var connection = ConnectionFactory.CreateConection(hostName);
        //    var chanel = ChanelFactory.CreateChanel(queueName, connection);
        //  //  var responseSenderChanel = ChanelFactory.CreateChanel(queueName, connection);
        //    var consumer =ConsumerFactory.CreateConsumer(chanel,queueName);

        //    chanel.BasicConsume(queue: queueName,
        //        autoAck: true,
        //        consumer: consumer);

        //    return new Listener(chanel, /*responseSenderChanel,*/ consumer,queueName,receivedHandler,responseSender);

        //}

        //public static Listener CreateListener(  IModel chanel , string queueName, EventHandler<BasicDeliverEventArgs> receivedHandler)
        //{
        //    var consumer = ConsumerFactory.CreateConsumer(chanel, queueName);
        //    return new Listener(chanel,   consumer ,queueName,receivedHandler);
        //}



    }
}
