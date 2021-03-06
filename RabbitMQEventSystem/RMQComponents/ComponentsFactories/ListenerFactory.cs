﻿using System;
using Newtonsoft.Json.Serialization;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace RabbitMQEventSystem.RMQComponents.ComponentsFactories
{
  public  class ListenerFactory
    {
        /// <summary>
        /// Creates and returns the Rabbit MQ Listener.
        /// </summary>
        /// <param name="hostName">Rabbit MQ host.</param>
        /// <param name="queueName"></param>
        /// <param name="receivedHandler"></param>
        /// <returns></returns>
        public static Listener CreateListener(string hostName,  string queueName, EventHandler<BasicDeliverEventArgs> receivedHandler)
        {
            var connection = ConnectionFactory.CreateConection( hostName);
            var chanel = ChanelFactory.CreateRequestChanel(queueName,connection);
            var consumer = ConsumerFactory.RequestConsumer(chanel, queueName);
            return new Listener(chanel, consumer, queueName, receivedHandler);
        }


        /// <summary>
        /// Creates and returns the Rabbit MQ Listener which can send call-backs
        /// </summary>
        /// <param name="hostName">Rabbit MQ host.</param>
        /// <param name="queueName"></param>
        /// <param name="receivedHandler"></param>
        /// <param name="binarySender"></param>
        /// <returns></returns>
        public static Listener CreateListener(string hostName, string queueName, EventHandler<BasicDeliverEventArgs> receivedHandler, ResponseBynarySender binarySender)
        {
            var connection = ConnectionFactory.CreateConection(hostName);
            var requestChanel = ChanelFactory.CreateRequestChanel(queueName, connection);
    

            var sender = PublisherFactory.CreateResponseSender(hostName, queueName, connection);

            var consumer = ConsumerFactory.RequestConsumer(requestChanel, queueName);
            return new Listener(requestChanel, consumer, queueName, receivedHandler,sender, binarySender);
        }


        /// <summary>
        /// Creates and returns the Rabbit MQ Listener which can send call-backs
        /// </summary>
        /// <param name="hostName">Rabbit MQ host.</param>
        /// <param name="queueName"></param>
        /// <param name="receivedHandler"></param>
        /// <param name="objectSender"></param>
        /// <returns></returns>
        public static Listener CreateListener(string hostName, string queueName, EventHandler<BasicDeliverEventArgs> receivedHandler, ResponseObjectSender objectSender)
        {
            var connection = ConnectionFactory.CreateConection(hostName);
            var requestChanel = ChanelFactory.CreateRequestChanel(queueName, connection);
            var sender = PublisherFactory.CreateResponseSender(hostName, queueName, connection);
            var consumer = ConsumerFactory.RequestConsumer(requestChanel, queueName);
            return new Listener(requestChanel, consumer, queueName, receivedHandler, sender, objectSender);
        }

 
    }
}
