using Repository.Entities;

namespace Chatroom.Repository.Interfaces
{
    public interface IChatRepository
    {
        Task<bool> ChatRoomExists(int id);
        Task<IList<ChatMessage>> GetMessages(int chatId);
        Task SendMessage(ChatMessage entity);
        Task SaveChatroom(ChatRoom chatRoom);
    }
}