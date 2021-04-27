using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace RequestRabbitMQ
{
    class Program
    {
        static void Main(string[] args)
        {
            var connectionFactory = new ConnectionFactory()
            {
                UserName = "guest",
                Password = "guest",
                HostName = "localhost"
            };

            var connection = connectionFactory.CreateConnection();
            var model = connection.CreateModel();
            var properties = model.CreateBasicProperties();
            properties.Persistent = false;

            byte[] messageBuffer = Encoding.UTF8.GetBytes("Direct Messages");
            byte[] messageTopic = Encoding.UTF8.GetBytes("Message from Topic Exchange 'Bombay'");
            byte[] messageFanout = Encoding.UTF8.GetBytes("Message is of fanout Exchange type"); 
            byte[] messageHeader = Encoding.UTF8.GetBytes("Message to Headers Exchange 'format=pdf' ");

            //-----------------------------publish message(direct exchange)-------------------------------------//

            //publish message (direct exchange)
            //model.ExchangeDeclare(".netDemoExchange", ExchangeType.Direct);
            //model.QueueDeclare(".netDemoQueue", true, false, false, null);
            //model.QueueBind(".netDemoQueue", ".netDemoExchange", "directExchange_key");
            //model.BasicPublish(".netDemoExchange", "directExchange_key", properties, messageBuffer);

            //-----------------------------publish message(topic exchange)-------------------------------------//

            //model.ExchangeDeclare("topic.exchange", ExchangeType.Topic);

            //model.QueueDeclare("topic.bombay.queue", true, false, false, null);
            //model.QueueDeclare("topic.delhi.queue", true, false, false, null);

            //model.QueueBind("topic.bombay.queue", "topic.exchange", "*.Bombay.*");
            //model.QueueBind("topic.delhi.queue", "topic.exchange", "Delhi.#");

            //model.BasicPublish("topic.exchange", "Message.Bombay.Email", properties, messageTopic);

            //-----------------------------publish message(fanout exchange)-------------------------------------//

            //model.ExchangeDeclare("fanout.exchange", ExchangeType.Fanout);

            //model.QueueDeclare("Mumbai", true, false, false, null);
            //model.QueueDeclare("Bangalore", true, false, false, null);
            //model.QueueDeclare("Chennai", true, false, false, null);
            //model.QueueDeclare("Hyderabad", true, false, false, null);

            //model.QueueBind("Mumbai", "fanout.exchange", String.Empty);
            //model.QueueBind("Bangalore", "fanout.exchange", String.Empty);
            //model.QueueBind("Chennai", "fanout.exchange", String.Empty);
            //model.QueueBind("Hyderabad", "fanout.exchange", String.Empty);

            //model.BasicPublish("fanout.exchange", "", properties, messageFanout);

            //-----------------------------publish message(headers exchange)-------------------------------------//
            model.ExchangeDeclare("headers.exchange", ExchangeType.Headers);

            model.QueueDeclare("ReportPDF", true, false, false, null);
            model.QueueDeclare("ReportExcel", true, false, false, null);

            Dictionary<string, object> arguments1 = new Dictionary<string, object>();
            arguments1.Add("format", "pdf");
            arguments1.Add("x-match", "all");

            properties.Headers = arguments1;

            model.QueueBind("ReportPDF", "headers.exchange", "");

            model.BasicPublish("headers.exchange", "", properties, messageHeader);
        }
    }
}
