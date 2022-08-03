using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using RabbitMQ.Client;

namespace RabbitMQProducer
{
    public static class QueueProducer
    {
        public static void Publish(IModel channel)
        {
            channel.QueueDeclare("demo-queue", durable: true, exclusive: false, autoDelete: false, null);

            int count = 0;
            while (true)
            {
                var message =new {Name="Producer" , Message= $"Hello Message Number {count}" } ;
                var body = JsonSerializer.SerializeToUtf8Bytes(message);
                channel.BasicPublish("", routingKey: "demo-queue", null,body);
                count++;
                Thread.Sleep(1000);
            }
            
        }
    }
}
