using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQ.Producer
{
    static class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory { Uri = new Uri("amqp://guest@localhost:5672") };

            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare("demo-queue", 
                                         durable: true, 
                                         exclusive: false, 
                                         autoDelete: false, 
                                         arguments: null);

                    var message = new { Name = "Producer", Message = "Hello!" };

                    var messageJson = JsonConvert.SerializeObject(message);

                    var body = Encoding.UTF8.GetBytes(messageJson);

                    channel.BasicPublish("", "demo-queue", null, body);

                    Console.WriteLine("Message Sent");
                }
            }
            Console.ReadLine();
        }
    }
}
