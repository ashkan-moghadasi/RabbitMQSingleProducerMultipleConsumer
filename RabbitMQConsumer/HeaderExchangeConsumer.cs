using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace RabbitMQConsumer
{
    public static class HeaderExchangeConsumer
    {
        public static void Consume(IModel channel)
        {
            channel.ExchangeDeclare("demo-header-exchange", ExchangeType.Headers);
            channel.QueueDeclare("demo-header-queue", durable: true, exclusive: false, autoDelete: false, null);
            var header = new Dictionary<string, object>()
            {
                {"account","new"}
            };
            channel.QueueBind("demo-header-queue", "demo-header-exchange", string.Empty,header);
            //Set PreFetch Count = 10 messages
            channel.BasicQos(0, 10, false);
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (sender, e) =>
            {
                var body = e.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine(message);
            };
            channel.BasicConsume("demo-header-queue", autoAck: true, consumer);
            Console.WriteLine("RabbitMQ Consumer Started");
            Console.ReadKey();
        }
    }
}