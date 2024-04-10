﻿using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RabbitMQ.Producer
{
    public static class TopicExchangeProducer
    {
        public static void Publish(IModel channel)
        {
            var ttl = new Dictionary<string, object>
            {
                { "x-message-ttl", 30000 }
            };

            channel.ExchangeDeclare("demo-topic-exchange", ExchangeType.Topic, arguments: ttl);

            var count = 0;

            while (true)
            {
                var message = new { Name = "Producer", Message = "Hello!", Count = count };
                var messageJson = JsonConvert.SerializeObject(message);
                var body = Encoding.UTF8.GetBytes(messageJson);

                channel.BasicPublish("demo-topic-exchange", "account.update", null, body);
                count++;

                Thread.Sleep(1000);
            }
        }
    }
}
