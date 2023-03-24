using Azure.Messaging.ServiceBus;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MSA.MessageBus
{
    public class AzureServiceBusMessageBus : IMessageBus
    {
        //to be improved
        private readonly string connectionString = "Endpoint=sb://msarestaurant.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=6hrwnRwK5vZn2Cf4HN09RTDQLUKnNq1aD+ASbBGEDTY=";

        public async Task PublishMessage(BaseMessage message, string topicName)
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
