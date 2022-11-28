using AutoMapper;
using Chatroom.Models;
using Chatroom.Repository.Interfaces;
using Chatroom.Services.Interfaces;
using Repository.Entities;

namespace Chatroom.Services
{
    public class ChatService : IChatService
    {
        private readonly IMapper mapper;
        private readonly IChatRepository chatRepository;
        private readonly IRabbitMQService rabbitMQService;

        public ChatService(IMapper mapper, IChatRepository chatRepository, IRabbitMQService rabbitMQService)
        {
            this.mapper = mapper;
            this.chatRepository = chatRepository;
            this.rabbitMQService = rabbitMQService;
        }

        public async Task<List<ChatMessageViewModel>> GetMessages(int chatId, string? name)
        {
            var message = rabbitMQService.ConsumerRabbitMQ(chatId);

            if (message.UserName != name)
            {
                await SendMessage(new ChatMessageViewModel
                {
                    Content = message.Content,
                    UserName = message.UserName,
                    ChatRoom = new ChatRoomViewModel { Id = chatId }
                });
            }

            return mapper.Map<List<ChatMessageViewModel>>(await chatRepository.GetMessages(chatId));
        }

        public async Task SendMessage(ChatMessageViewModel message)
        {
            await VerifyChatRoom(message.ChatRoom);

            if (!string.IsNullOrWhiteSpace(message.Content))
            {
                using (var client = new HttpClient())
                {
                    await rabbitMQService.SendMessage(message);
                }

                await chatRepository.SendMessage(mapper.Map<ChatMessage>(message));
            }
            }

        private async Task VerifyChatRoom(ChatRoomViewModel chatRoom)
        {
            if (!await chatRepository.ChatRoomExists(chatRoom.Id))
                await chatRepository.SaveChatroom(mapper.Map<ChatRoom>(chatRoom));
        }
    }
}
