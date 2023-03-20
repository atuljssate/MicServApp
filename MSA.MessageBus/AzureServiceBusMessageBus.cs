﻿using Azure.Messaging.ServiceBus;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSA.MessageBus
{
    internal class AzureServiceBusMessageBus : IMessageBus
    {
        //to be improved
        private string connectionString = "";

        public async Task PublicMessage(BaseMessage message, string topicName)
        {
           await using var client= new ServiceBusClient(connectionString);
            ServiceBusSender sender = client.CreateSender(topicName);
            var jsonMessage=JsonConvert.SerializeObject(message);
            ServiceBusMessage busMessage = new ServiceBusMessage(Encoding.UTF8.GetBytes(jsonMessage))
            {
                CorrelationId = Guid.NewGuid().ToString()
            };
            await sender.SendMessageAsync(busMessage);
            await client.DisposeAsync();
        }
    }
}
