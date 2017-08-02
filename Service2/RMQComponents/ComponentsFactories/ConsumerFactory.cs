using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Service2.RMQComponents.ComponentsFactories
{
   public class ConsumerFactory
    {
        /// <summary>
        /// Creaye RabbitMq event responseConsumer
        /// </summary>
        /// <param name="chanel"></param>
        /// <param name="receivedHandler"></param>
        /// <param name="responseSender"></param>
        /// <returns></returns>
        public static EventingBasicConsumer CreateConsumer(IModel chanel, string queueName)
        {
            var consumer = new EventingBasicConsumer(chanel);
            chanel.BasicConsume(queue: queueName,
                autoAck: false,
                consumer: consumer);

            return consumer;
        }

      

       
    }
}
