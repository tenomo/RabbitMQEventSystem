using System;
using System.Threading.Tasks;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Service2.RMQComponents
{
    public class Listener
    {
        /// <summary>
        /// ListenerChanel to RabbitMq service
        /// </summary>
        public IModel ListenerChanel { get; private set; }

        /// <summary>
        /// ListenerChanel to RabbitMq service
        /// </summary>
    //    public IModel ResponseSenderChanel { get; private set; }

        /// <summary>
        /// RabbitMq event consumer.
        /// </summary>
        public EventingBasicConsumer Consumer { get; private set; }

        private readonly string queueName;

        private readonly EventHandler<BasicDeliverEventArgs> ReceivedHandler;

        private readonly ResponseSender ResponseSender;


        internal Listener(IModel listenerChanel, EventingBasicConsumer consumer, string queueName,
            EventHandler<BasicDeliverEventArgs> receivedHandler)
        {
            this.ListenerChanel = listenerChanel;
            this.Consumer = consumer;
            this.queueName = queueName;
            this.ReceivedHandler = receivedHandler;

            this.Consumer.Received += Consumer_Received;
        }

        internal Listener(IModel listenerChanel/*, IModel responseSenderChanel*/, EventingBasicConsumer consumer,
            string queueName, EventHandler<BasicDeliverEventArgs> receivedHandler, ResponseSender responseSender)
        {
            this.ListenerChanel = listenerChanel;
            //this.ResponseSenderChanel = responseSenderChanel;
            this.Consumer = consumer;
            this.queueName = queueName;
            this.ReceivedHandler = receivedHandler;
            this.ResponseSender = responseSender;

            this.Consumer.Received += Consumer_Received;
        }

        private void Consumer_Received(object sender, BasicDeliverEventArgs e)
        { 
            Task.Factory.StartNew(() =>
            {
                if (this.ReceivedHandler != null && ExtensionMethods.isEventType(e.BasicProperties.Type, EventType.Request))  //ExtensionMethods.isEventType(e.RoutingKey) == EventType.Request)
                { 
                    this.ReceivedHandler(sender, e);
                    ResponseSender?.Invoke(e, ListenerChanel, EventType.Response.ToString()); /*ExtensionMethods.CreateRoutinKey(queueName, EventType.Response)*/
                    //this.ResponseSender?.Invoke(e, this.ResponseSenderChanel,
                    //    ExtensionMethods.CreateRoutinKey(this.queueName, EventType.Response));
                    Consumer.HandleBasicConsumeOk(Consumer.ConsumerTag);
                }
              
            });
        }
    }
}
