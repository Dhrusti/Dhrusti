using DTO;

namespace ServiceLayer.Interface
{
    public interface IChatClient
    {
        Task ReceiveMessage(ChatMessage message);
        Task SendMessage(ChatMessage message);
    }
}
