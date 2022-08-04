using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace RabbitMQConsumer
{
    public static class DirectExchangeConsumer
    {
        public static void Consume(IModel channel)
        {
            channel.ExchangeDeclare("demo-direct-exchange",ExchangeType.Direct);
            channel.QueueDeclare("demo-direct-queue", durable: true, exclusive: false, autoDelete: false, null);
            channel.QueueBind("demo-direct-queue", "demo-direct-exchange","account.init");
            //Set PreFetch Count = 10 messages
            channel.BasicQos(0,10,false);
            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (sender, e) =>
            {
                var body = e.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine(message);
            };
            channel.BasicConsume("demo-direct-queue", autoAck: true, consumer);
            Console.WriteLine("RabbitMQ Consumer Started");
            Console.ReadKey();
        }
    }
}
