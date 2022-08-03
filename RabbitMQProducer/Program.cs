// See https://aka.ms/new-console-template for more information

using RabbitMQ.Client;
using RabbitMQProducer;

Console.WriteLine("RabbitMQ Producer Started");
var factory = new ConnectionFactory()
{
    Uri = new Uri("amqp://guest:guest@localhost:5672")
};
var connection = factory.CreateConnection();
var channel = connection.CreateModel();
QueueProducer.Publish(channel);
