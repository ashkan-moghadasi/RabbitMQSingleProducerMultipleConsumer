using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace RabbitMQConsumer
{
    public static class FanoutExchangeConsumer
    {
        public static void Consume(IModel channel)
        {
            channel.ExchangeDeclare("demo-fanout-exchange", ExchangeType.Fanout);
            var queueName = channel.QueueDeclare().QueueName;

            channel.QueueBind(queueName, "demo-fanout-exchange", string.Empty);
            //Set PreFetch Count = 10 messages
            channel.BasicQos(0, 10, false);
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (sender, e) =>
            {
                var body = e.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine(message);
            };
            channel.BasicConsume(queueName, autoAck: true, consumer);
            Console.WriteLine("RabbitMQ Consumer Started");
            Console.ReadKey();
        }
    }
}