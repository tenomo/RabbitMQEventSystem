using  System;
using System.Runtime.InteropServices;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace RabbitMQEventSystem.RMQComponents
{ 
   public enum EventType
    {
        Request,
        Response
    }

    /// <summary>
    /// Send client response.
    /// </summary>
    /// <param name="consumer"></param>
    /// <param name="chanel"></param>
    public delegate byte [] ResponseBynarySender(BasicDeliverEventArgs e  );

    /// <summary>
    /// Send client response.
    /// </summary>
    /// <param name="consumer"></param>
    /// <param name="chanel"></param>
    public delegate object  ResponseObjectSender(BasicDeliverEventArgs e);


    public static class ExtensionMethods
    {
        //public static EventType isEventType(string routingKey)
        //{
        //    if (routingKey.ToUpper().Contains(EventType.Request.ToString().ToUpper()))
        //        return EventType.Request;
        //    else if (routingKey.ToUpper().Contains(EventType.Response.ToString().ToUpper()))
        //    {
        //        return EventType.Response;
        //    }
        //    else
        //    {
        //        throw new ArgumentException("Invalid routing key, it is not contains EventType");
        //    }
        //}

        public static bool isEventType(string queueName, EventType idial)
        {

            var isEventType = queueName.Contains(idial.ToString());

            return isEventType;
        }


        public static string GetRequestQueueName(string queueName)
        {
            return ExtensionMethods.CreateRoutinKey(queueName, EventType.Request);
        }

        public static string GetResponseQeueName(string queueName)
        {
            return ExtensionMethods.CreateRoutinKey(queueName, EventType.Response);
        }
        public static string CreateRoutinKey(string queueName, EventType key)
        {
            var rkey = queueName + key;
            return rkey;
        }
    }

}
 