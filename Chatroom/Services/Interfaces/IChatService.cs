using Chatroom.Models;

namespace Chatroom.Services.Interfaces
{
    public interface IChatService
    {
        Task<List<ChatMessageViewModel>> GetMessages(int chatId, string? name);
        Task SendMessage(ChatMessageViewModel message);
    }
}