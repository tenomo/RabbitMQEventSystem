using  System;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Service2.RMQComponents
{ 
   public enum RoutingKeyModifier
    {
        Request,
        Response
    }

    /// <summary>
    /// Send client response.
    /// </summary>
    /// <param name="consumer"></param>
    /// <param name="chanel"></param>
    public delegate void ResponseSender(BasicDeliverEventArgs e, IModel chanel, string routingKey);

    public static class ExtensionMethods
    {
        public static RoutingKeyModifier GetEventType(string routingKey)
        {
            if (routingKey.ToUpper().Contains(RoutingKeyModifier.Request.ToString().ToUpper()))
                return RoutingKeyModifier.Request;
            else if (routingKey.ToUpper().Contains(RoutingKeyModifier.Response.ToString().ToUpper()))
            {
                return RoutingKeyModifier.Response;
            }
            else
            {
                throw new ArgumentException("Invalid routing key, it is not contains RoutingKeyModifier");
            }
        }

        public static string CreateRoutinKey( string queueName, RoutingKeyModifier key)
        {
            return queueName + key;
        }
    }

}
 