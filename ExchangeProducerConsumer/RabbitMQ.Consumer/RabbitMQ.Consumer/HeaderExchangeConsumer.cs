using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQ.Consumer
{
    public static class HeaderExchangeConsumer
    {
        public static void Consume(IModel channel)
        {
            channel.ExchangeDeclare("demo-header-exchange", ExchangeType.Headers);
            channel.QueueDeclare("demo-header-queue",
                                         durable: true,
                                         exclusive: false,
                                         autoDelete: false,
                                         arguments: null);

            var header = new Dictionary<string, object> { { "account", "new" } };

            channel.QueueBind("demo-header-queue", "demo-header-exchange", string.Empty, header);

            channel.BasicQos(0, 10, false);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (sender, ea) =>
            {
                var body = ea.Body.ToArray();
                var messageJson = Encoding.UTF8.GetString(body);
                var message = JsonConvert.DeserializeObject<dynamic>(messageJson);

                Console.WriteLine($"Received message: {message.Name} - {message.Message} Count: {message.Count}");
            };

            channel.BasicConsume("demo-header-queue", true, consumer);

            Console.ReadLine();
        }
    }
}
