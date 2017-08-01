using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Service2.RMQComponents;
using Service2.RMQComponents.ComponentsFactories;

namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            var conn = Service2.RMQComponents.ComponentsFactories.ConnectionFactory.CreateConection("localhost");
            var listener = ListenerFactory.CreateListener(ChanelFactory.CreateChanel("test_lib",conn), "test_lib", ReceivedHandler/*, ResponseSender */);
            Console.ReadKey();
        }

        private static void ResponseSender(BasicDeliverEventArgs basicDeliverEventArgs, IModel chanel, string routingKey)
        {
            var props = chanel.CreateBasicProperties();
            props.ContentType = routingKey;
            chanel.BasicPublish(exchange: "",
                       routingKey: routingKey,
                       basicProperties: props, //new BasicProperties() {ContentType = "request"},
                       body: Encoding.UTF8.GetBytes("\n[x]Handled "));
        }

        private static void ReceivedHandler(object sender, BasicDeliverEventArgs basicDeliverEventArgs)
        {
            Console.WriteLine("\n[x]Handle: " + Encoding.UTF8.GetString(basicDeliverEventArgs.Body));
        }
    }
}
