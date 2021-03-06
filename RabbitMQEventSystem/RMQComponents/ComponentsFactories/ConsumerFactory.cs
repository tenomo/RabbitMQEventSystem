﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace RabbitMQEventSystem.RMQComponents.ComponentsFactories
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
        public static EventingBasicConsumer RequestConsumer(IModel chanel, string queueName)
        {
            var consumer = new EventingBasicConsumer(chanel);
            chanel.BasicConsume(queue: ExtensionMethods.GetRequestQueueName(queueName),
                autoAck: true,
                consumer: consumer);

            return consumer;
        }


        /// <summary>
        /// Creaye RabbitMq event responseConsumer
        /// </summary>
        /// <param name="chanel"></param>
        /// <param name="receivedHandler"></param>
        /// <param name="responseSender"></param>
        /// <returns></returns>
        public static EventingBasicConsumer ReponceConsumer(IModel chanel, string queueName)
        {
            var consumer = new EventingBasicConsumer(chanel);
            chanel.BasicConsume(queue: ExtensionMethods.GetResponseQeueName(queueName),
                autoAck: true,
                consumer: consumer);

            return consumer;
        }

    }
}
