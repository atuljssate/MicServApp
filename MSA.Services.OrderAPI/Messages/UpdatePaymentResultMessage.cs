﻿using MSA.MessageBus;

namespace MSA.Services.OrderAPI.Messages
{
    public class UpdatePaymentResultMessage : BaseMessage
    {
        public int OrderId { get; set; }  
        
        public bool Status { get; set; }
    }
}
