﻿namespace MSA.MessageBus
{
    public interface IMessageBus
    {
        Task PublicMessage(BaseMessage message, string topicName);

    }
}