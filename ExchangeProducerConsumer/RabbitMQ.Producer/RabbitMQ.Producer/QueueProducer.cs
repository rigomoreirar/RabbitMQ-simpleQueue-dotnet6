using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RabbitMQ.Producer
{
    public static class QueueProducer
    {
        public static void Publish(IModel channel) 
        {
            channel.QueueDeclare("demo-queue",
                                  durable: true,
                                  exclusive: false,
                                  autoDelete: false,
                                  arguments: null);

            var count = 0;

            while (true)
            {
                var message = new { Name = "Producer", Message = "Hello!", Count = count };
                var messageJson = JsonConvert.SerializeObject(message);
                var body = Encoding.UTF8.GetBytes(messageJson);

                channel.BasicPublish("", "demo-queue", null, body);
                count++;

                Thread.Sleep(1000);
            }

            

        }
    }
}
