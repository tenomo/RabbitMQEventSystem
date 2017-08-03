using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQEventSystem.RMQComponents.ComponentsFactories;
namespace RabbitMQEventSystem.RMQComponents
{
   public class Publisher
    {
        ///// <summary>
        ///// Connection to RabbitMq service.
        ///// </summary>
      //  public IConnection Connection { get; private set; }
        public string QueueName { get; private set; }
       
        public IModel RequestChanel { get; private set; }
        public IModel ResponseChanel { get; private set; }
        
        internal EventType Type { get;  set; }  = EventType.Request;
        private string RoutiongKey => ExtensionMethods.CreateRoutinKey(QueueName, Type);

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
            var properties = RequestChanel.CreateBasicProperties();
            properties.Persistent = true;

           
            serializationStream.Dispose();

            RequestChanel.BasicPublish(exchange: "",
                routingKey: RoutiongKey,
                basicProperties: properties, 
                body: binaryRequest);
        }

        public void Publish(byte [] body)
        {
            var properties = RequestChanel.CreateBasicProperties();
            properties.Persistent = true;
            RequestChanel.BasicPublish(exchange: "",
                routingKey: RoutiongKey,
                basicProperties: properties,
                body: body);
        }


         
        internal Publisher( IModel requestChanel, string queueName )
        {
            Type = EventType.Request;  
            this.RequestChanel = requestChanel; 
            QueueName = queueName;


        }

        internal Publisher( IModel requestChanel, IModel responseChanel, string queueName, EventHandler<BasicDeliverEventArgs> reciveResponse, EventingBasicConsumer consumer)
        {
            Type = EventType.Request; 
            this.RequestChanel = requestChanel;
            this.Consumer = consumer;
            QueueName = queueName;
            ReciveResponse = reciveResponse;
            Consumer.Received += Consumer_Received;
        }

        private void Consumer_Received(object sender, BasicDeliverEventArgs e)
        {
            if (this.ReciveResponse != null /*&& ExtensionMethods.isEventType(e.RoutingKey,EventType.Response)*/)
            {
                ReciveResponse(sender, e);
              Consumer?.HandleBasicCancelOk(Consumer.ConsumerTag);
                Consumer?.HandleBasicConsumeOk(Consumer.ConsumerTag);
                ResponseChanel?.BasicAck(e.DeliveryTag, false);
            }
            else throw new ArgumentException("ReciveResponse must be not null");
        }
    }
}
