using System.Threading.Tasks;

namespace MSA.MessageBus
{
    public interface IMessageBus
    {
        Task PublishMessage(BaseMessage message, string topicName);

    }
}