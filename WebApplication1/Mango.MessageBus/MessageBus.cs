﻿using Azure.Messaging.ServiceBus;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Mango.MessageBus
{
    public class MessageBus : IMessageBus
    {
        private string connectionString = "Endpoint=sb://mangoweb-app.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=gs3puvIQgtYkPzh/iWw0alEc8SapBHCYS+ASbKNwk00=";

        public async Task PublishMessage(object message, string topic_queue_Name)
        {

            string value = Environment.GetEnvironmentVariable("ServiceBusConnString");


            Console.WriteLine($"Test1: {value}\n");

            await using var client = new ServiceBusClient(connectionString);

            ServiceBusSender sender=client.CreateSender(topic_queue_Name);

            var jsonMessage=JsonConvert.SerializeObject(message);
            ServiceBusMessage finalMessage=new ServiceBusMessage(Encoding.UTF8.GetBytes(jsonMessage)) 
            {
                CorrelationId = Guid.NewGuid().ToString(),
            };

            await sender.SendMessageAsync(finalMessage);
            await client.DisposeAsync();

        }
    }
}
