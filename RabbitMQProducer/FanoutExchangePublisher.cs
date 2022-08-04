using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using RabbitMQ.Client;

namespace RabbitMQProducer;

public static class FanoutExchangePublisher
{
    public static void Publish(IModel channel)
    {
        //Set Time To Live Messages in Exchange
        var ttl = new Dictionary<string, object>()
        {
            {"x-message-ttl", 30000}
        };
        channel.ExchangeDeclare("demo-fanout-exchange", ExchangeType.Fanout);

        int count = 0;
        while (true)
        {
            var message = new { Name = "Producer", Message = $"Hello Message Number {count}" };
            var body = JsonSerializer.SerializeToUtf8Bytes(message);
            var properties = channel.CreateBasicProperties();
            properties.Headers=new Dictionary<string, object>()
            {
                {"account","new"}
            };
            channel.BasicPublish("demo-fanout-exchange", routingKey: string.Empty, null,body);
            count++;
            Thread.Sleep(1000);
        }

    }

}