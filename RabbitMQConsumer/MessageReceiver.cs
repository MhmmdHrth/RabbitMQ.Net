using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace RabbitMQConsumer
{
    public class MessageReceiver : DefaultBasicConsumer
    {
        private readonly IModel channel;

        public MessageReceiver(IModel channel)
        {
            this.channel = channel;
        }

        public override void HandleBasicDeliver(string consumerTag, ulong deliveryTag, bool redelivered, string exchange, string routingKey, IBasicProperties properties, ReadOnlyMemory<byte> body)
        {
            Console.WriteLine("Consuming Message");
            Console.WriteLine($"Message receiver from the exchange: {exchange}");
            Console.WriteLine($"Consumer Tag: {consumerTag}");
            Console.WriteLine($"Delivery Tag: {deliveryTag}");
            Console.WriteLine($"Routing Tag from Exchange: {routingKey}");
            Console.WriteLine($"Message: {Encoding.UTF8.GetString(body.ToArray())}");
            channel.BasicAck(deliveryTag, false);
        }
    }
}
