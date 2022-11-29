using Chatroom.Models;
using Repository.Entities;

namespace Chatroom.Test
{
    public class MockHelper
    {
        public static List<ChatMessage> GetMessages()
        {
            return new List<ChatMessage>()
            {
                new ChatMessage()
                {
                    Id = 1,
                    Content = "Test",
                    UserName = "User name test",
                    TimeStamp = DateTime.Today,
                    Chatroom = new ChatRoom
                    {
                        Id = 1,
                    }
                },
            };
        }

        internal static List<ChatMessageViewModel> GetListChatMessageViewModel()
        {
            return new List<ChatMessageViewModel>()
            {
                new ChatMessageViewModel()
                {
                    Id = 1,
                    Content = "Test",
                    UserName = "User name test",
                    ChatRoom = new ChatRoomViewModel
                    {
                        Id = 1,
                    }
                },
            };
        }
    }
}
