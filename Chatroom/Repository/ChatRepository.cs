using Chatroom.Data;
using Chatroom.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Repository.Entities;

namespace Chatroom.Repository
{
    public class ChatRepository : IChatRepository
    {
        private readonly ApplicationDbContext context;
        public ChatRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<IList<ChatMessage>> GetMessages(int chatId)
        {
            return await context.ChatMessage
                .Where(x => x.Chatroom.Id == chatId)
                .OrderBy(x => x.TimeStamp)
                .Take(50)
                .ToListAsync();
        }

        public async Task SendMessage(ChatMessage chatMessage)
        {
            await context.ChatMessage.AddAsync(chatMessage);
            context.SaveChanges();
        }

        public async Task SaveChatroom(ChatRoom chatRoom)
        {
            await context.ChatRoom.AddAsync(chatRoom);
            context.SaveChanges();
        }

        public async Task<bool> ChatRoomExists(int id)
        {
            return context.ChatRoom.Any(x => x.Id == id);
        }
    }
}
