﻿using System;
using System.Collections.Generic;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace RabbitMQEventSystem.RMQComponents.ComponentsFactories
{
    public   class PublisherFactory
    {         
        public static Publisher CreatePublisher(string hostName, string queueName)
        {
            var connection = ConnectionFactory.CreateConection(hostName);
            var requestChanel = ChanelFactory.CreateRequestChanel(queueName, connection);
            return new Publisher(  requestChanel, queueName);
        }

        public static Publisher CreateResponseSender(string hostName, string queueName)
        {
            var connection = ConnectionFactory.CreateConection(hostName);
            var responseChanel = ChanelFactory.CreateRequestChanel(queueName, connection);
           var responseSender = new Publisher(responseChanel, queueName);
            responseSender.Type = EventType.Response;
            return responseSender;
        }



        public static Publisher CreateResponseSender(string hostName, string queueName, IConnection connection)
        { 
            var responseChanel = ChanelFactory.CreateRequestChanel(queueName, connection);
            var responseSender = new Publisher(responseChanel, queueName) {Type = EventType.Response};
            return responseSender;
        }


        public static Publisher CreatePublisher(string hostName, string queueName,
            EventHandler<BasicDeliverEventArgs> responseHandler)
        {
            var connection = ConnectionFactory.CreateConection(hostName);
            var requestChanel = ChanelFactory.CreateRequestChanel(queueName, connection);
            var responseChanel = ChanelFactory.CreateResponseChanel(queueName, connection);
            var consumer = ConsumerFactory.ReponceConsumer(responseChanel,  queueName);
            return new Publisher(requestChanel,responseChanel, queueName, responseHandler, consumer);
        }


        public static Publisher CreatePublisher(string hostName, string queueName, IDictionary<string, object> arguments)
        {
            var connection = ConnectionFactory.CreateConection(hostName);
            var requestChanel = ChanelFactory.CreateRequestChanel(queueName, connection, arguments);
            
            return new Publisher(requestChanel, queueName);
        }



        public static Publisher CreatePublisher(string hostName, string queueName,
            EventHandler<BasicDeliverEventArgs> responseHandler, IDictionary<string, object> arguments)
        {
            var connection = ConnectionFactory.CreateConection(hostName);
            var requestChanel = ChanelFactory.CreateRequestChanel(queueName, connection, arguments);
            var responseChanel = ChanelFactory.CreateResponseChanel(queueName, connection, arguments);
            var consumer = ConsumerFactory.ReponceConsumer(responseChanel, queueName);
            return new Publisher(requestChanel, responseChanel, queueName, responseHandler, consumer);
        }



    }
}
