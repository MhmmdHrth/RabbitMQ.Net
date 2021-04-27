using RabbitMQ.Client;
using System;

namespace RabbitMQConsumer
{
    class Program
    {
        static void Main(string[] args)
        {
            ConnectionFactory connectionFactory = new ConnectionFactory
            {
                HostName = "localhost",
                UserName = "guest",
                Password = "guest",
            };

            var connection = connectionFactory.CreateConnection();
            var channel = connection.CreateModel();

            //prefetchCount means don't dispatch a new message to a worker with specific number, until it has processed and acknowledged 
            channel.BasicQos(0, 1, false);
            MessageReceiver messageReceiver = new MessageReceiver(channel);

            //direct exchange
            //channel.BasicConsume(".netDemoQueue", false, messageReceiver);

            //topic exchange
            //channel.BasicConsume("topic.bombay.queue", false, messageReceiver);

            //fanout exchange
            //channel.BasicConsume("Mumbai", false, messageReceiver);

            //header exchange
            channel.BasicConsume("ReportPDF", false, messageReceiver);
            Console.ReadLine();
        }
    }
}
