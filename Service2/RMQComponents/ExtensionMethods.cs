using  System;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Service2.RMQComponents
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
    public delegate void ResponseSender(BasicDeliverEventArgs e, IModel chanel, string contentType);

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

        public static bool isEventType(string eventType, EventType idial)
        {
            try
            {
                EventType eType;
                EventType.TryParse(eventType, out eType);
                return idial == eType;
            }
            catch (ArgumentException e)
            {
                throw new ArgumentException("Invalid routing key, it is not contains EventType", e);
            }


        }

        //public static string CreateRoutinKey( string queueName, EventType key)
        //{
        //    var rkey = queueName + key;
        //    return rkey;
        //}
    }

}
 