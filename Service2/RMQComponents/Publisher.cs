using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Service2.RMQComponents
{
   public class Publisher
    {
        /// <summary>
        /// Connection to RabbitMq service.
        /// </summary>
        public IConnection Connection { get; private set; }

        public string QueueName { get; private set; }
        /// <summary>
        /// ListenerChanel to RabbitMq service
        /// </summary>
        public IModel Chanel { get; private set; }


        /// <summary>
        /// RabbitMq event responseConsumer.
        /// </summary>
        public EventingBasicConsumer Consumer { get; private set; }

        private   EventHandler<BasicDeliverEventArgs> ReciveResponse;

        public void Publish (object body )
        {
            var formatter = new BinaryFormatter();
            var serializationStream = new MemoryStream();
            formatter.Serialize(serializationStream, body);
            var binaryRequest = serializationStream.ToArray();
            var properties = Chanel.CreateBasicProperties();
            properties.Persistent = true;
            properties.Type= EventType.Request.ToString();
         
            serializationStream.Dispose();

            Chanel.BasicPublish(exchange: "",
                routingKey: QueueName,//ExtensionMethods.CreateRoutinKey(QueueName,EventType.Request),
                basicProperties: properties, 
                body: binaryRequest);
        }



        internal Publisher(IConnection connection, IModel chanel, string queueName )
        {
            this.Chanel = chanel;
            this.Connection = connection; 
            QueueName = queueName;


        }

        internal Publisher(IConnection connection, IModel chanel, string queueName, EventingBasicConsumer responseConsumer, EventHandler<BasicDeliverEventArgs> reciveResponse)
        {
            this.Chanel = chanel;
            this.Connection = connection;
            this.Consumer = responseConsumer;
            QueueName = queueName;
            ReciveResponse = reciveResponse;
            Consumer.Received += Consumer_Received;
        }

        private void Consumer_Received(object sender, BasicDeliverEventArgs e)
        {
            if (this.ReciveResponse != null && ExtensionMethods.isEventType(e.BasicProperties.Type,EventType.Response))
            {
                ReciveResponse(sender, e);
            }
        }
    }
}
