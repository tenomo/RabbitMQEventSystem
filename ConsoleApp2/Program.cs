using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
            Console.WriteLine("Press any kay to start");
            Console.ReadKey();
            var conn = Service2.RMQComponents.ComponentsFactories.ConnectionFactory.CreateConection("localhost");
            //var listener = ListenerFactory.CreateListener(ChanelFactory.CreateChanel("test_lib",conn), "test_lib", ReceivedHandler, ResponseSender );
            var listener = ListenerFactory.CreateListener("localhost", "test_lib", ReceivedHandler);//, BinarySender);



            var Publisher = PublisherFactory.CreatePublisher("localhost", "test_lib");//, responseHandler);

            for (int i = 0; i < 20; i++)
            {
                object id = i;
                new TaskFactory().StartNew(() =>
                {
                    Thread.Sleep(3000);
                    Publisher.Publish("Hello " + id);
                    Console.WriteLine("[x] Send: " + "Hello " + id);
                });

            }
            Console.WriteLine("Press any kay to exit");
            Console.ReadKey();
        }

        private static byte[] BinarySender(BasicDeliverEventArgs basicDeliverEventArgs)
        {
            return Encoding.UTF8.GetBytes("\n[x]Handled request");
        }

        private static void ReceivedHandler(object sender, BasicDeliverEventArgs basicDeliverEventArgs)
        {
            Console.WriteLine("\n[x]Handle: " + Encoding.UTF8.GetString(basicDeliverEventArgs.Body));
        }


        private static void responseHandler(object sender, BasicDeliverEventArgs basicDeliverEventArgs)
        {
            Console.WriteLine("\t\t" + Encoding.UTF8.GetString(basicDeliverEventArgs.Body));
        }

    }
}
