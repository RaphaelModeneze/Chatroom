using Chatroom.Models;

namespace Chatroom.Services.Interfaces
{
    public interface IRabbitMQService
    {
        Task SendMessage(ChatMessageViewModel message);
        ChatMessageViewModel? ConsumerRabbitMQ(int chatId);
    }
}