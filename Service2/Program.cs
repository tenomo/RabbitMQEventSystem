//using System;
//using System.IO;
//using System.Runtime.Serialization.Formatters.Binary;
//using System.Threading;
//using System.Threading.Tasks;
//using System.Text;
//using RabbitMQ.Client;
//using RabbitMQ.Client.Events;
//using RabbitMQ.Client.Framing;
//using DTO;
//using Service2.RMQComponents;
//using Service2.RMQComponents.ComponentsFactories;

//namespace Service2
//{
//    // Listener
//    class Program
//    {
//        private static readonly Random RANDOM = new Random();
//        private static readonly int THREADS_COUNT = 20;
//        //private static readonly string TEST_QUEUE_1 = "TEST_QUEUE_1";
//        //private static readonly string TEST_QUEUE_2 = "TEST_QUEUE_2";

//        static void Main(string[] args)
//        {
//           // ThreadPoolRestrictions(THREADS_COUNT);



//            //          Console.WriteLine("Press any key to start");
//            //          Console.ReadLine();
//            //          var factory = new ConnectionFactory() { HostName = "localhost", SocketReadTimeout = 30000, SocketWriteTimeout = 30000 };




//            //          var connection = factory.CreateConnection();
//            //          var channel = connection.CreateModel();



//            //channel.QueueDeclare(queue: TEST_QUEUE_2,
//            //              durable: false,
//            //              exclusive: false,
//            //              autoDelete: false,
//            //              arguments: null);


//            //          var consumer = new EventingBasicConsumer(channel);
//            //          channel.BasicConsume(queue: TEST_QUEUE_2,
//            //    autoAck: true,
//            //    consumer: consumer);


//            //          consumer.Received += (model, e) =>
//            //          {

//            //              new TaskFactory().StartNew(() =>
//            //              {
//            //                  var caclTime = RANDOM.Next(500, 1500);
//            //                  Thread.Sleep(caclTime);

//            //                  var e_body = e.Body;


//            //                  var requestMemoryStream = new MemoryStream(e_body);
//            //                  var requestFromater = new BinaryFormatter();
//            //                  var e_request = requestFromater.Deserialize(requestMemoryStream) as Request;

//            //                  Console.WriteLine("[x] handle: " + e_request.ToString() + ";  thread id [" +
//            //                                    Thread.CurrentThread.ManagedThreadId + "]" + " work emulation time[" +
//            //                                    caclTime + "]");



//            // answer

//            //     Response response = new Response()
//            //    {
//            //        Id = e_request.Id,
//            //        Message1 = e_request.Message,
//            //        Message2 = "Message is handled"
//            //    };




//            //    var responseMemoryStream = new MemoryStream();
//            //    var responseFromater = new BinaryFormatter();
//            //    responseFromater.Serialize(responseMemoryStream, response);
//            //    var binaryResponse = responseMemoryStream.ToArray();

//            //    consumer.HandleBasicCancelOk(consumer.ConsumerTag);


//            //    channel.BasicPublish(exchange: "",
//            //        routingKey: TEST_QUEUE_1,
//            //        basicProperties: new BasicProperties() { ContentType = "response" },
//            //        body: binaryResponse);

//            //    responseMemoryStream.Dispose();
//            //});


//            //    });
//            //};


            

//            Console.WriteLine("[x] Press any key to start listen");
//            Console.ReadLine();
//            //var rb_mq_conecton = RMQComponents.ComponentsFactories.ConnectionFactory.CreateConection("localhost");

//            //var rb_mq_chanel = ChanelFactory.CreateChanel(TEST_QUEUE_2, rb_mq_conecton);
//            //var rb_mq_consumer = ConsumerFactory.CreateConsumer(rb_mq_chanel, TEST_QUEUE_2, ReceivedHandler, ResponseSender);

//            //var rabbitMqListener = ListenerFactory.CreateListener(rb_mq_conecton, rb_mq_chanel, rb_mq_consumer);


//    var listener = ListenerFactory.CreateListener("localhost", "test_lib", ReceivedHandler, ResponseSender);
//            var Publisher = PublisherFactory.CreatePublisher("localhost", "test_lib", ReceivedResponse);
        
    

//            for (int i = 0; i < 20; i++)
//            {
//                object id = i;
//                new TaskFactory().StartNew(() => {
//                    Thread.Sleep(3000);
//                    Publisher.Publish("Hello " + id);
//                    Console.WriteLine("[x] Send: " + "Hello " + id);
//                });
          
//            }

//            Console.WriteLine("[x] Press any key to exit");
//            Console.ReadLine();
         
//        }

//        private static void ReceivedHandler(object sender, BasicDeliverEventArgs basicDeliverEventArgs)
//        {
//            Console.WriteLine("\n[x]Handle: " + Encoding.UTF8.GetString(basicDeliverEventArgs.Body));
//        }

//        private static void ResponseSender(BasicDeliverEventArgs basicDeliverEventArgs, IModel chanel, string routingKey)
//        {
//            chanel.BasicPublish(exchange: "",
//                       routingKey: routingKey,
//                       basicProperties: chanel.CreateBasicProperties(), //new BasicProperties() {ContentType = "request"},
//                       body: Encoding.UTF8.GetBytes("\n[x]Handled "));
//        }

//        private static void ReceivedResponse(object sender, BasicDeliverEventArgs basicDeliverEventArgs)
//        {
//            Console.WriteLine(Encoding.UTF8.GetString(basicDeliverEventArgs.Body));
//        }


        

//        //public static void ResponseSender(BasicDeliverEventArgs responseData, IModel chanel)
//        //{
//        //    var requestMemoryStream = new MemoryStream(responseData.Body);
//        //    var requestFromater = new BinaryFormatter();
//        //    var e_request = requestFromater.Deserialize(requestMemoryStream) as Request;

//        //    Response response = new Response()
//        //    {
//        //        Id = e_request.Id,
//        //        Message1 = e_request.Message,
//        //        Message2 = "Message is handled"
//        //    };

//        //    var responseMemoryStream = new MemoryStream();
//        //    var responseFromater = new BinaryFormatter();
//        //    responseFromater.Serialize(responseMemoryStream, response);
//        //    var binaryResponse = responseMemoryStream.ToArray();
//        //    chanel.BasicPublish(exchange: "",
//        //        routingKey: TEST_QUEUE_1,
//        //        basicProperties: null,
//        //        body: binaryResponse);

//        //    responseMemoryStream.Dispose();
//        //}

//        //public static void ReceivedHandler(object sender, BasicDeliverEventArgs e)
//        //{
//        //    var consumer = sender as EventingBasicConsumer;
//        //    var caclTime = RANDOM.Next(2000, 3000); Thread.Sleep(caclTime);
//        //    var requestMemoryStream = new MemoryStream(e.Body);
//        //    var requestFromater = new BinaryFormatter();
//        //    var e_request = requestFromater.Deserialize(requestMemoryStream) as Request;
//        //    Console.WriteLine($"[x][x] handle: {e_request.ToString()}; thread id [{Thread.CurrentThread.ManagedThreadId}] work emulation time[{caclTime}]");
//        //    //  requestMemoryStream.Dispose();
//        //    consumer.HandleBasicConsumeOk(consumer.ConsumerTag);


//        //}


//        private static void ThreadPoolRestrictions(int threadCount)
//        {
//            ThreadPool.SetMinThreads(threadCount, threadCount);
//            ThreadPool.SetMaxThreads(threadCount, threadCount);
//        }
//    }
//}
