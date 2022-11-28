namespace Chatroom.Models
{
    public class ChatMessageViewModel
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public string UserName { get; set; }
        public ChatRoomViewModel ChatRoom { get; set; }
        public DateTime TimeStamp => DateTime.Now;

    }
}
