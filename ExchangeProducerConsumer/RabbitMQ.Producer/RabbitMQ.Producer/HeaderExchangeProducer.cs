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
    public static class HeaderExchangeProducer
    {
        public static void Publish(IModel channel)
        {
            var ttl = new Dictionary<string, object>
            {
                { "x-message-ttl", 30000 }
            };

            channel.ExchangeDeclare("demo-header-exchange", ExchangeType.Headers, arguments: ttl);

            var count = 0;

            while (true)
            {
                var message = new { Name = "Header", Message = "Hello!", Count = count };
                var messageJson = JsonConvert.SerializeObject(message);
                var body = Encoding.UTF8.GetBytes(messageJson);

                var properties = channel.CreateBasicProperties();
                properties.Headers = new Dictionary<string, object> { { "account", "update" } };

                channel.BasicPublish("demo-header-exchange", string.Empty, properties, body);
                count++;

                Thread.Sleep(1000);
            }
        }
    }
}
