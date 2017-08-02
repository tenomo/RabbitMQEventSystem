using System;
using System.Threading.Tasks;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Service2.RMQComponents
{
    public class Listener
    {

        private readonly Publisher responceSender;
        private readonly EventHandler<BasicDeliverEventArgs> ReceivedHandler;
        private readonly EventingBasicConsumer consumer;
        private readonly ResponseObjectSender objectSender;
        private readonly ResponseBynarySender binarySender;


        public IModel ListenerChanel { get; private set; }
        public  string QueueName { get;  private set; }

        

        internal Listener(IModel listenerChanel, EventingBasicConsumer consumer, string queueName,
            EventHandler<BasicDeliverEventArgs> receivedHandler)
        {
            this.ListenerChanel = listenerChanel;
            this.consumer = consumer;
            this.QueueName = queueName;
            this.ReceivedHandler = receivedHandler;

             this.consumer.Received += Consumer_Received;
        }

        

        internal Listener(IModel listenerChanel, EventingBasicConsumer consumer,
     string queueName, EventHandler<BasicDeliverEventArgs> receivedHandler, Publisher responceSender, ResponseBynarySender binarySender )
        {
            this.ListenerChanel = listenerChanel;
            this.consumer = consumer;
            this.QueueName = queueName;
            this.ReceivedHandler = receivedHandler;
            this.responceSender = responceSender;
            this.binarySender = binarySender;
               this.consumer.Received += Consumer_Received;
        }


        internal Listener(IModel listenerChanel, EventingBasicConsumer consumer,
     string queueName, EventHandler<BasicDeliverEventArgs> receivedHandler, Publisher responceSender, ResponseObjectSender objectSender)
        {
            this.ListenerChanel = listenerChanel;
            this.consumer = consumer;
            this.QueueName = queueName;
            this.ReceivedHandler = receivedHandler;
            this.responceSender = responceSender;
            this.objectSender = objectSender;
            this.consumer.Received += Consumer_Received;
        }


        /// <summary>
        /// Event handler.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Consumer_Received(object sender, BasicDeliverEventArgs e)
        {
            Task.Factory.StartNew(() =>
            {
                try
                {
                    if (this.ReceivedHandler == null) throw new ArgumentException("ReceivedHandler must be not null");
                    //if (ExtensionMethods.isEventType(e.RoutingKey, EventType.Request))
                    //{
                    //    throw new ArgumentException("Routing key must be containe \"Request\"");
                    //}
                    this.ReceivedHandler(sender, e);
   //consumer.HandleBasicConsumeOk(consumer.ConsumerTag);
               //    consumer.HandleBasicCancelOk(consumer.ConsumerTag);
                 //   ListenerChanel.BasicAck(e.DeliveryTag, false);

                    if (binarySender != null)
                    {
                        var binaryBody = binarySender.Invoke(e);
                        responceSender.Publish(binaryBody);
                    }
                    else if (objectSender != null)
                    {
                        var objectBody = objectSender.Invoke(e);
                        responceSender.Publish(objectBody);
                    }
                   
                }
                catch (ArgumentException exception)
                {
                    consumer.HandleBasicCancel(consumer.ConsumerTag);
                    throw exception;
                }

            });
        }
    }
}
