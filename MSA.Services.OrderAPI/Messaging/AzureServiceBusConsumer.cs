using Azure.Messaging.ServiceBus;
using MSA.Services.OrderAPI.Messages;
using MSA.Services.OrderAPI.Models;
using MSA.Services.OrderAPI.Repository;
using Newtonsoft.Json;
using System.Text;

namespace MSA.Services.OrderAPI.Messaging
{
    public class AzureServiceBusConsumer: IAzureServiceBusConsumer
    {
        private readonly OrderRepository _orderRepository;

        private readonly string serviceBusConnectionString;
        private readonly string subscriptionCheckOut;
        private readonly string checkoutMessageTopic;
        
        private ServiceBusProcessor checkOutProcessor;

        private readonly IConfiguration _configuration;

        public AzureServiceBusConsumer(OrderRepository orderRepository, IConfiguration configuration)
        {
            _orderRepository = orderRepository; 
            _configuration = configuration;
            serviceBusConnectionString = _configuration.GetValue<string>("ServiceBusConnectionString");
            subscriptionCheckOut = _configuration.GetValue<string>("SubscriptionCheckOut");
            checkoutMessageTopic = _configuration.GetValue<string>("CheckoutMessageTopic");

            var client= new ServiceBusClient(serviceBusConnectionString);
            checkOutProcessor=client.CreateProcessor(checkoutMessageTopic, subscriptionCheckOut);
        }

        public async Task Start()
        {
            checkOutProcessor.ProcessMessageAsync += OnCheckOutMessageReveived;
            checkOutProcessor.ProcessErrorAsync += ErrorHandler;
            await checkOutProcessor.StartProcessingAsync();
        }

        public async Task Stop()
        {
            await checkOutProcessor.StopProcessingAsync();
            await checkOutProcessor.DisposeAsync();
        }

        Task ErrorHandler(ProcessErrorEventArgs args)
        {
            Console.WriteLine(args.Exception.ToString());
            return Task.CompletedTask;  
        }
        private async Task OnCheckOutMessageReveived(ProcessMessageEventArgs args) 
        { 
            var message = args.Message;
            var body= Encoding.UTF8.GetString(message.Body);

            CheckoutHeaderDto checkoutHeaderDto = JsonConvert.DeserializeObject<CheckoutHeaderDto>(body);

            OrderHeader orderHeader = new()
            {
                UserId = checkoutHeaderDto.UserId,
                FirstName = checkoutHeaderDto.FirstName,
                LastName = checkoutHeaderDto.LastName,
                OrderDetails = new List<OrderDetails>(),
                CardNumber = checkoutHeaderDto.CardNumber,
                CouponCode = checkoutHeaderDto.CouponCode,
                CVV = checkoutHeaderDto.CVV,
                DiscountTotal = checkoutHeaderDto.DiscountTotal,
                Email = checkoutHeaderDto.Email,
                ExpiryMonthYear = checkoutHeaderDto.ExpiryMonthYear,
                OrderTime = DateTime.Now,
                OrderTotal = checkoutHeaderDto.OrderTotal,
                PaymentStatus = false,
                Phone = checkoutHeaderDto.Phone,
                PickUpDateTime = checkoutHeaderDto.PickUpDateTime
            };
            foreach (var detailList in orderHeader.OrderDetails) 
            {
                OrderDetails orderDetails = new()
                {
                    ProductId = detailList.ProductId,
                    ProductName = detailList.ProductName,
                    Price = detailList.Price,
                    Count = detailList.Count
                };

                orderHeader.CartTotalItems += detailList.Count;
                orderHeader.OrderDetails.Add(orderDetails);
            }

            await _orderRepository.AddOrder(orderHeader);

        }
    }
}
